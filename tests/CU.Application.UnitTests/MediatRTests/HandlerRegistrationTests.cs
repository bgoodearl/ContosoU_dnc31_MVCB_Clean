using CU.Application.SchoolItems.Departments.Queries;
using CU.Application.Shared.DataRequests.SchoolItems.Queries;
using FluentAssertions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CU.Application.UnitTests.MediatRTests
{
    public class HandlerRegistrationTests
    {
        /// <summary>
        /// Thanks to https://matthiaslischka.at/2019/02/25/Testing-MediatR-Registrations/
        /// </summary>
        [Fact]
        public void AllRequests_ShouldHaveMatchingHandler()
        {
            var requestTypes = typeof(GetDepartmentListItemsQuery).Assembly.GetTypes()
            .Where(IsRequest)
            .ToList();
            requestTypes.Should().NotBeNullOrEmpty();

            var handlerTypes = typeof(GetDepartmentListItemsQueryHandler).Assembly.GetTypes()
                .Where(IsIRequestHandler)
                .ToList();
            handlerTypes.Should().NotBeNullOrEmpty();

            foreach (var requestType in requestTypes) ShouldContainHandlerForRequest(handlerTypes, requestType);
        }

        private static bool IsHandlerForRequest(Type handlerType, Type requestType)
        {
            return handlerType.GetInterfaces().Any(i => i.GenericTypeArguments.Any(ta => ta == requestType));
        }

        private static bool IsRequest(Type type)
        {
            return typeof(IBaseRequest).IsAssignableFrom(type);
        }

        private static bool IsIRequestHandler(Type type)
        {
            return type.GetInterfaces().Any(interfaceType => interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IRequestHandler<,>));
        }
        private static void ShouldContainHandlerForRequest(IEnumerable<Type> handlerTypes, Type requestType)
        {
            handlerTypes.Should().ContainSingle(handlerType => IsHandlerForRequest(handlerType, requestType), $"Handler for type {requestType} expected");
        }
    }
}
