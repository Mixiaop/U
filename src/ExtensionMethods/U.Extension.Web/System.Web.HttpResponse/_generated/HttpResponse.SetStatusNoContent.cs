// Copyright (c) 2015 ZZZ Projects. All rights reserved
// Licensed under MIT License (MIT) (https://github.com/zzzprojects/Z.ExtensionMethods)
// Website: http://www.zzzprojects.com/
// Feedback / Feature Requests / Issues : http://zzzprojects.uservoice.com/forums/283927
// All ZZZ Projects products: Entity Framework Extensions / Bulk Operations / Extension Methods /Icon Library

using System.Web;

public static partial class Extensions
{
    /// <summary>
    ///     A HttpResponse extension method that sets the response to status code 204 (No content.).
    /// </summary>
    /// <param name="this">The @this to act on.</param>
    public static void SetStatusNoContent(this HttpResponse @this)
    {
        @this.StatusCode = 204;
        @this.StatusDescription = "No content.";
    }
}