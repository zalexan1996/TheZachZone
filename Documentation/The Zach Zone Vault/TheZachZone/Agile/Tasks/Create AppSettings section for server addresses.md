
> [!summary] Story
> Currently, server addresses are stored in plaintext in the code. I'd like for them to be extracted to a section in appsettings.json.

> [!important] Out of Scope
> Provide things that are not required for this task...

> [!done] Acceptance Criteria
> - All server plaintext references are replaced with data pulled from the IOptions API
> - 

> [!info] Implementation Details
> ``` c# 
> public class ServerAddressSettings
> {
> 	public int OTELPort { get; set;}
> 	public string TheZachZone { get; set; }
> 	public string TheGameZone { get; set; }
> }
> ```

> [!seealso] See Also
> Provide related information...

> [!quote] Data
> [StoryType:: Task]
> [Story-Points:: 3] 
> [Site:: All]
> [Priority:: Low]


