
> [!summary] Story
> Create the build and release pipelines in Azure DevOps.

> [!done] Acceptance Criteria
> - There are two sets of pipelines. One for production and one for development
> - The development pipelines run when the development branch is pushed to
> - The production pipelines run when the production branch is pushed to.
> - An Azure agent is running on a server in my laundry room. This is the server that will be hosting the site. 
> - The local server will be running docker images and use nginx to reverse proxy the sites on the containers. 
> - HTTPS works
> - Hyperlinks between my sites still work.
> - Nginx hosts each site using the dryrlent.ddns.net/{siteName}

> [!info] Implementation Details
> Provide information on how to accomplish this task...

> [!seealso] See Also
> [Installing nginx on Windows](https://nginx.org/en/docs/windows.html)

> [!quote] Data
> [StoryType:: Task]
> [Story-Points:: 8] 
> [Site:: All]
> [Priority:: High]