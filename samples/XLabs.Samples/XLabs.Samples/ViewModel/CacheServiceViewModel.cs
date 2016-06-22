// ***********************************************************************
// Assembly         : XLabs.Sample
// Author           : XLabs Team
// Created          : 12-27-2015
// 
// Last Modified By : XLabs Team
// Last Modified On : 01-04-2016
// ***********************************************************************
// <copyright file="CacheServiceViewModel.cs" company="XLabs Team">
//     Copyright (c) XLabs Team. All rights reserved.
// </copyright>
// <summary>
//       This project is licensed under the Apache 2.0 license
//       https://github.com/XLabs/Xamarin-Forms-Labs/blob/master/LICENSE
//       
//       XLabs is a open source project that aims to provide a powerfull and cross 
//       platform set of controls tailored to work with Xamarin Forms.
// </summary>
// ***********************************************************************
// 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using XLabs.Caching;
using XLabs.Ioc;

namespace XLabs.Samples.ViewModel
{
    /// <summary>
    /// Sample ViewModel for the CacheService.
    /// </summary>
    public class CacheServiceViewModel : XLabs.Forms.Mvvm.ViewModel
    {
        /// <summary>
        /// Key used in the cache.
        /// </summary>
        private const string DEMO_KEY = "test-key";

        /// <summary>
        /// Holds a reference to the CacheService.
        /// </summary>
        private readonly ICacheProvider _cacheService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CacheServiceViewModel"/> class.
        /// </summary>
        public CacheServiceViewModel()
        {
            _cacheService = Resolver.Resolve<ICacheProvider>();
            CheckKeyAndDownloadNewContent();
        }

        /// <summary>
        /// Downloads items to the cache based on a key.
        /// </summary>
        private void CheckKeyAndDownloadNewContent()
        {
            if (_cacheService == null)
            {
                throw new ArgumentNullException(
                    "_cacheService",
                    new Exception("Native SimpleCache implementation wasn't found."));
            }

            var keyValue = _cacheService.Get<List<string>>(DEMO_KEY);
            if (keyValue != null)
            {
                CacheInfo = "key found on cache";
                Items = new ObservableCollection<string>(keyValue);
            }
            else
            {
                CacheInfo = "key wasn't found on cache, you can save  it now";
                Items = new ObservableCollection<string> { "Bananas", "Oranges", "Apples" };
            }
        }

        /// <summary>
        /// Backing field for the CacheInfo property.
        /// </summary>
        private string _cacheInfo;

        /// <summary>
        /// Gets or sets a <see cref="string"/> with cache information.
        /// </summary>
        /// <value>The cache information.</value>
        public string CacheInfo
        {
            get
            {
                return _cacheInfo;
            }
            set
            {
                this.SetProperty(ref _cacheInfo, value);
            }
        }

        /// <summary>
        /// Backing field for the Items property.
        /// </summary>
        private ObservableCollection<string> _items;

        /// <summary>
        /// Gets or sets a collection of items in the cache.
        /// </summary>
        /// <value>The items in the cache.</value>
        public ObservableCollection<string> Items
        {
            get
            {
                return _items;
            }
            set
            {
                this.SetProperty(ref _items, value);
            }
        }

        /// <summary>
        /// Backing field to save items to the cache.
        /// </summary>
        private Command _saveItemsToCacheCommand;

        /// <summary>
        /// Gets the command to save items to the cache.
        /// </summary>
        /// <value>A <see cref="Command"/> to save items to the cache.</value>
        public Command SaveItemsToCacheCacheCommand
        {
            get
            {
                return _saveItemsToCacheCommand ?? (_saveItemsToCacheCommand = new Command(
                    () =>
                    {
                        _cacheService.Remove(DEMO_KEY);
                        _cacheService.Add(DEMO_KEY, Items.ToList());
                        CacheInfo = "key was saved on cache";
                    },
                    () => true));
            }
        }

        /// <summary>
        /// Backing field to clear items from the cache.
        /// </summary>
        private Command _clearCacheCommand;

        /// <summary>
        /// Gets the command to clear items from the cache.
        /// </summary>
        /// <value>A <see cref="Command"/> to clear items from the cache.</value>
        public Command ClearCacheCommand
        {
            get
            {
                return _clearCacheCommand ?? (_clearCacheCommand = new Command(
                     () => _cacheService.FlushAll(),
                    () => true));
            }
        }
    }
}