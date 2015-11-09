﻿using System;
using System.Diagnostics;

namespace NSquared2.DbC.ValidationExtenstions
{
    public static class ObjectValidationExtenstion
	{
		[DebuggerHidden]
		public static Validation<T> NotNull<T>(this Validation<T> item) where T : class
		{
			if (item.Value == null)
				throw new ArgumentNullException($"InputParam '{item.ParameterName}' cannot be null");

			return item;
		}

		[DebuggerHidden]
		public static Validation<T> IsOfType<T>(this Validation<T> item, Type type) where T : class
		{
			if (type.IsInstanceOfType(item))
				throw new ArgumentException($"InputParam '{item.ParameterName}' is not of type '{type}'");

			return item;
		}

		[DebuggerHidden]
		public static Validation<T> Must<T>(this Validation<T> item, Predicate<T> predicate) where T : class
		{
			if (predicate == null)
				throw new NullReferenceException($"Predicate for Must validation on item '{item.ParameterName}' cannot be null!");

			if (predicate.Invoke(item.Value))
				return item;

			throw new ArgumentException(
			    $"Validator '{predicate.Method.Name}' did not meet the expected validation cireteria for '{item.ParameterName}'");
		}
	}
}