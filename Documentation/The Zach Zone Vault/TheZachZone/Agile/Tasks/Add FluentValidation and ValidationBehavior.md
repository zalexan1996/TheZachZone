
> [!summary] Story
> I need a validation framework to shortcircuit CQRS commands via a pipeline behavior. Validation failures will be reported to ITelemetryService's Logger. 

> [!important] Out of Scope
> Provide things that are not required for this task...

> [!done] Acceptance Criteria
> - `AbstractValidators` are registered dynamically
> - A `ValidationBehavior` is ran before every command
> - Errors are logged to `ITelemetryService` and the command is not allowed.
> - Retroactively add `AbstractValidators` to every command.

> [!info] Implementation Details
> Provide information on how to accomplish this task...

> [!seealso] See Also
> Provide related information...

> [!quote] Data
> [StoryType:: Task]
> [Story-Points:: 8] 
> [Site:: All]
> [Priority:: High]


