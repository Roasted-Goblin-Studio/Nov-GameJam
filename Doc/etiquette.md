
# Branching 
Create a feature branch with your name which you can use through the Jam. 
- `feature/wiggles` <-- branch name

Commit often, we want that history babyyy

Create a PR when you're ready to merge your code with the `develop` branch, then reach out to Phil or Weston for approval

# Programming contribution etiquette
## Simplified Single line prefernces
### Single line with no open/close braces
```
if(foo) return false;
```
## Curly Brace Structure (classic C#)
### Devoted lines for open and close curly braces
```
void foo()
{
  /* ...
   * ...
   * ...
   */
}

if (bar)
{
  /* ...
   * ...
   * ...
   */
}
```
## Variable naming
### Class scope
- `private` variables use `_LeadingCamalCaseFormat`
- `public` variabels use `CamalCaseFormat` 

The reason we use the public format specifically is because it matches our function naming convention. Think of them like simple get;set functions. 

### Function scope
- All function scope variables use `snakeCaseFormat`

### Readability
- `boolean` values that track flags and state should be names with an "Is" prefix
```
private bool _IsMoving;
private bool _IsGrounded;
```

