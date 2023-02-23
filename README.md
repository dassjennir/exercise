# UrlShortener

Below is a list of funcionalities that haven't been implemented, but would be if it would be real application. I ommited them for the sake of simplicity and time. Two first are the most crucial.

This application IS NOT "concurrency safe". It doesn't lock or performs checks during database manipulation/reading. For real app some mechanism should be implemetented - locking, optimistic concurrency (.NET's [ConcurrencyCheck] or [Timestamp]), maybe changed isolation level in the database (could be even "Serializable" in SQL Server, because bussiness logic will be perfomed very fast).

Testing is not done thorough and fully, there are a lot to test left, because of time. I hope it's sufficient.

Only request logging. Should be some more logging at least to the console and/or file and logging appsetting should precise logging level (PROD/DEV). Information about creation requests are stored in the log file due to requirement "Application should store logs information about requests.". It wasn't precised where it should be stored, I thought for a moment about database, but I wasn't sure what you want.

No authentication nor authorization. In this case only to "admin panel".

Frontend tests - I do not have knowledge about frontend testing.

For such simple application, no complicated architectural patterns are needed (e.g. DDD). Repository pattern is implemented, but non-generic and without unit of work pattern (unnecessary in our case, but might be useful if funcionalities grow). Repository returns concrete classes, filtering on IQueryable is in repository. Some could argue if all bussiness logic actions should be placed in single service (UrlService), but in such such case I think it is sufficient. Client is Blazor WebAssembly. So models are placed in 3 places - client-only models (presentation models), server-only models (data, domain) and shared models (DTOs). Server models could be divided to data models and domain models, but it is a simple application, I didn't want to overcomplicate this.



Other info:

I use .NET 6, but pre-.NET 6  Visual Studio template style, with Startup and Program. I like it much more than new "all in Program" template.

Returning short URL only as code, not full URL due to ease of evaluating it as a job interview execrise. Normally, domain for short URL should be added and passed to the client, but in this case it would require checking from on what domain service is started. Normally it is localhost:5001, but the evlauator (you ;) ) can also start it on some other port and it would break. In production or even development environment we would use real domain.

Controller action error handling for creating short URL is done in a exception filter, although it is unnecessary in this case, but it is more universal if functionalities will be extended (for example action that lets the user to send more than 1 URL).

I see that reference nullable is set (<Nullable>enable</Nullable>). I don't know if it was a requirement to use it, but it wasn't written anywhere and I am not a big fan of this feature, so I didn't use it mostly. But I didn't enabled it, so warnings are left.

NumeralSystemConversion - normally would be compiled to separate DLL, thus, not placed in src folder.

## How to start?
`Section description:  What should be done before application start(e.g db migration)`
## Key assumptions 
`Section description: If you have any assumption during your implementation, please provide them here.`

## Future Ideas
`Section description:  If you haven't enough time to implement some feature or ideas, please provide them here.`


## Task Description 
>Build a URL shortening service like TinyURL. This service will provide short aliases redirecting to long URLs.
### Why do we use Url shortening?
URL shortening is used to create shorter aliases for long URLs. We call these shortened aliases “short links.” Users are redirected to the original URL when they hit these short links. Short links save a lot of space when displayed, printed, messaged, or tweeted. Additionally, users are less likely to mistype shorter URLs.

For example, if we shorten the following URL: `https://www.some-website.io/realy/long-url-with-some-random-string/m2ygV4E81AR`

We would get something like this: `https://short.url/xer23`

URL shortening is used to optimize links across devices, track individual links to analyze audience, measure ad campaigns’ performance, or hide affiliated original URLs.

### URL shortening application should have:
 - A page where a new URL can be entered and a shortened link is generated. You can use Home page.
 - A page that will show a list of all the shortened URL’s.
### Functional Requirements:
- Given a URL, our service should generate a shorter and unique alias of it. This is called a short link. This link should be short enough to be easily copied and pasted into applications.
- When users access a short link, our service should redirect them to the original link.
- Application should store logs information about requests.
### Non-Functional Requirements:
- URL redirection should happen in real-time with minimal latency.
- Please add small project description to Readme.md file.
### During implementation please pay attention to:
- Application is runnable out of box. If some setup is needed please provide details on ReadMe file.
- Project structure and code smells.
- Design Principles and application testability.
- Main focus should be on backend functionality, not UI.
- Input parameter validation.
- Please, don't use any library or service that implements the core functionality of this test.
### Other recommendation:
- You may change UI technology to any other you are most familiar with.
- You can use InMemory data storage.
- You can use the Internet.
# May the force be with you {username}!