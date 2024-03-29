﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DomainModel.Repository
{
   /// <summary>
    /// Base interface for an entity Repository. This interface includes the basic CRUD operations
    /// </summary>
    /// <typeparam name="TEntity">The TEntity type</typeparam>
    /// <typeparam name="TKey">The type of the TEntity key.</typeparam>
    public interface IRepository<TEntity, TKey>
        where TEntity : class
    {
        /// <summary>
        /// Gets an <see cref="IQueryable{T}"/> for all Entities.
        /// </summary>
        /// <returns>An <see cref="IQueryable{TEntity}"/> for all Entities</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Gets an <see cref="IPagedList{TEntity}"/> for the all entities.
        /// </summary>
        /// <param name="pageIndex">
        /// The required page number (0 based).
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="sortingCriteria">A coma separated list of property/field names from <see cref="TEntity"/>
        /// of the ordering being specified, followed optionally, by ascending or descending string. If empty string or <c>null</c>,
        /// the default (undetermined) sort criteria is used.
        /// <example>
        /// "Date", "Date ascending" - "Date, Kind descending".
        /// </example>
        /// </param>
        /// <returns>
        /// A <see cref="IPagedList{T}"/> for all Entities
        /// </returns>
//        IPagedList<TEntity> GetAll(int pageIndex, int pageSize, string sortingCriteria);

        /// <summary>
        /// Gets an <see cref="IPagedList{TEntyty}"/> for the all entities complying with the specified <paramref name="filterCriteria"/>.
        /// </summary>
        /// <param name="filterCriteria">
        /// The filter criteria that all returned entities must fulfill.
        /// </param>
        /// <param name="pageIndex">
        /// The required page number (0 based).
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="sortingCriteria">A coma separated list of property/field names from <typeparamref name="TEntity"/>
        /// of the ordering being specified, followed optionally, by ascending or descending string. If empty string or <c>null</c>,
        /// the default (undetermined) sort criteria is used.
        /// <example>
        /// "Date", "Date ascending" - "Date, Kind descending".
        /// </example>
        /// </param>
        /// <returns>
        /// A <see cref="IPagedList{Photographer}"/> for all photographers.
        /// </returns>
//        IPagedList<TEntity> GetAll(string filterCriteria, int pageIndex, int pageSize, string sortingCriteria);

        /// <summary>
        /// Gets an entity given the specified <paramref name="entityId"/>.
        /// </summary>
        /// <param name="entityId">The entity id.</param>
        /// <returns>The <typeparamref name="TEntity"/> if it exists, <code>null</code> otherwise </returns>
        TEntity Get(TKey entityId);

        /// <summary>
        /// Creates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        void Add(TEntity entity);

        /// <summary>
        /// Updates the specified entity.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        void Update(TEntity entity);

        /// <summary>
        /// Deletes the specified entity.
        /// </summary>
        /// <param name="entityId">The key of the entity to delete.</param>
        void Delete(TKey entityId);

    }
}
