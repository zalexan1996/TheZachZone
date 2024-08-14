Used for resolving the Static Files folder and building the game hosting paths.


> [!summary] Story
> I need a service responsible for resolving server-side file paths.

> [!important] Out of Scope
> - Ensuring that the folders exist.

> [!done] Acceptance Criteria
> - The service must be registered in WebShared

> [!info] Implementation Details
> Since all commands and queries are in Core, the interface itself needs to be a little bloated and have methods for paths required by all sites. If it gets too unwieldy, I'll switch to multiple services.

> [!seealso] See Also
> Provide related information...

> [!quote] Data
> [StoryType:: Task]
> [Story-Points:: 3] 
> [Site:: All]
> [Priority:: High]


