﻿using Microsoft.Extensions.DependencyInjection;

namespace HotChocolate.PersistedQueries.FileSystem;

public class RequestExecutorBuilderTests
{
    [Fact]
    public void AddFileSystemQueryStorage_2_Services_Is_Null()
    {
        // arrange
        // act
        void Action()
            => HotChocolateFileSystemPersistedQueriesRequestExecutorBuilderExtensions
                .AddFileSystemOperationDocumentStorage(null!);

        Assert.Throws<ArgumentNullException>(Action);
    }
}
