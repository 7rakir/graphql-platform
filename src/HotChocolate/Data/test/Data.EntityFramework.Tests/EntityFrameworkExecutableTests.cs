using CookieCrumble;

namespace HotChocolate.Data;

public class EntityFrameworkExecutableTests(AuthorFixture authorFixture)
    : IClassFixture<AuthorFixture>
{
    private readonly BookContext _context = authorFixture.Context;

    [Fact]
    public void Extensions_Should_ReturnEntityFrameworkExecutable_When_DBSet()
    {
        // arrange
        // act
        var executable = _context.Authors.AsExecutable();

        // assert
        Assert.True(executable is IQueryableExecutable<Author>);
        executable.MatchSnapshot();
    }

    [Fact]
    public void Extensions_Should_ReturnEntityFrameworkExecutable_When_Queryable()
    {
        // arrange
        // act
        var executable = _context
            .Authors
            .AsQueryable()
            .AsEntityFrameworkExecutable();


        // assert
        Assert.True(executable is IQueryableExecutable<Author>);
        executable.MatchSnapshot();
    }

    [Fact]
    public async Task ExecuteAsync_Should_ReturnAllItems_When_ToListAsync()
    {
        // arrange
        var executable = _context
            .Authors
            .AsExecutable();

        // act
        object result = await executable.ToListAsync(default);

        // assert
        new { result, executable = executable.Print(), }.MatchSnapshot();
    }

    [Fact]
    public async Task ExecuteAsync_Should_OnlyOneItem_When_SingleOrDefault()
    {
        // arrange
        IExecutable executable = _context.Authors.Take(1).AsEntityFrameworkExecutable();

        // act
        var result = await executable.SingleOrDefaultAsync(default);

        // assert
        new { result, executable = executable.Print(), }.MatchSnapshot();
    }

    [Fact]
    public async Task ExecuteAsync_Should_OnlyOneItem_When_FirstOrDefault()
    {
        // arrange
        IExecutable executable = _context.Authors.AsExecutable();

        // act
        var result = await executable.FirstOrDefaultAsync(default);

        // assert
        new { result, executable = executable.Print(), }.MatchSnapshot();
    }
}
