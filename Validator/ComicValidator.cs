using FluentValidation;
using System;
using System.Text.RegularExpressions;
using ComicMvC.Models;

public class ComicValidator : AbstractValidator<Comic>
{
    public ComicValidator()
    {
        // Title is required and must not exceed 255 characters.
        RuleFor(c => c.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(255).WithMessage("Title cannot exceed 255 characters.");

        // Issue number must be between 1 and 1000.
        RuleFor(c => c.IssueNumber)
            .InclusiveBetween(1, 1000).WithMessage("Issue number must be between 1 and 1000.");

        // Release date cant be in future.
        RuleFor(c => c.ReleaseDate)
            .LessThanOrEqualTo(DateTime.Today).WithMessage("Release date cannot be in the future.");

        // make sure a valid Publisher is selected.
        RuleFor(c => c.PublisherId)
            .GreaterThan(0).WithMessage("A publisher must be selected.");

        // make sure a valid Genre is selected.
        RuleFor(c => c.GenreId)
            .GreaterThan(0).WithMessage("A genre must be selected.");

        //if cover image URL is provided, it should be valid URL.
        RuleFor(c => c.CoverImageUrl)
            .Must(BeAValidUrl)
            .When(c => !string.IsNullOrEmpty(c.CoverImageUrl))
            .WithMessage("Cover image URL must be valid.");
    }

    private bool BeAValidUrl(string url)
    {
        // Basic URL validation using Regex.
        // You can use a more robust URL validation if needed.
        return Regex.IsMatch(url, @"^(https?://)?([\da-z\.-]+)\.([a-z\.]{2,6})([/\w \.-]*)*/?$");
    }
}
