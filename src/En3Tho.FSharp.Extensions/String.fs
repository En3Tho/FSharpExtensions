module En3Tho.FSharp.Extensions.String

open System

let inline nullOrWhiteSpaceCheck argName arg = if String.IsNullOrWhiteSpace(arg) then nullArg argName |> ignore
let inline nullOrEmptyCheck argName arg = if String.IsNullOrEmpty(arg) then nullArg argName |> ignore
let inline ensureNotNullOrWhiteSpace argName arg = if String.IsNullOrWhiteSpace(arg) then nullArg argName else arg
let inline ensureNotNullOrEmpty argName arg = if String.IsNullOrEmpty(arg) then nullArg argName else arg
let inline defaultValue def str = if String.IsNullOrEmpty(str) then def else str
let inline defaultValueW def str = if String.IsNullOrWhiteSpace(str) then def else str
let inline defaultWith ([<InlineIfLambda>] defThunk) str = if String.IsNullOrEmpty(str) then defThunk() else str
let inline defaultWithW ([<InlineIfLambda>] defThunk) str = if String.IsNullOrWhiteSpace(str) then defThunk() else str
let inline truncate maxLength (str: string) = if str.Length <= maxLength then str else str.Substring(0, maxLength)

let inline (|NullOrEmpty|_|) (str: string) =
    String.IsNullOrEmpty(str)
let inline (|NotNullOrEmpty|_|) (str: string) =
    String.IsNullOrEmpty(str) |> not
let inline (|NullOrWhiteSpace|_|) (str: string) =
    String.IsNullOrWhiteSpace(str)
let inline (|NotNullOrWhiteSpace|_|) (str: string) =
    String.IsNullOrWhiteSpace(str) |> not

let inline (|Equals|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.Ordinal))
let inline (|EqualsIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.OrdinalIgnoreCase))
let inline (|EqualsCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCulture))
let inline (|EqualsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCultureIgnoreCase))
let inline (|EqualsInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCulture))
let inline (|EqualsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCultureIgnoreCase))
let inline (|EqualsChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Length = 1 && str[0] = pattern)

let inline (|NotEquals|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.Ordinal) |> not)
let inline (|NotEqualsIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.OrdinalIgnoreCase) |> not)
let inline (|NotEqualsCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCulture) |> not)
let inline (|NotEqualsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCultureIgnoreCase) |> not)
let inline (|NotEqualsInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCulture) |> not)
let inline (|NotEqualsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCultureIgnoreCase) |> not)
let inline (|NotEqualsChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Length = 1 && str[0] = pattern |> not)

let private equalsAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <-  str.Equals(pattern, comparison)
    found

let private equalsAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str.Equals(pattern, comparison)
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private equalsAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
         let pattern = enumerator.Current
         found <- str.Equals(pattern, comparison)
    found

let private equalsAnyCharArray (patterns: char[]) (str: string) =
    if str.Length > 1 then false else
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <- str[0] = pattern
    found

let private equalsAnyCharList (patterns: char list) (str: string) =
    if str.Length > 1 then false else
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str[0] = pattern
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private equalsAnyCharSeq (patterns: char seq) (str: string) =
    if str.Length > 1 then false else
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
         let pattern = enumerator.Current
         found <- str[0] = pattern
    found

let equalsAny (patterns: string seq) (comparison: StringComparison) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (string[]) as patterns -> equalsAnyArray patterns comparison str
        | :? (string list) as patterns -> equalsAnyList patterns comparison str
        | _ -> equalsAnySeq patterns comparison str

let equalsAnyChar (patterns: char seq) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (char[]) as patterns -> equalsAnyCharArray patterns str
        | :? (char list) as patterns -> equalsAnyCharList patterns str
        | _ -> equalsAnyCharSeq patterns str

let inline (|EqualsAny|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.Ordinal
let inline (|EqualsAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.OrdinalIgnoreCase
let inline (|EqualsAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCulture
let inline (|EqualsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCultureIgnoreCase
let inline (|EqualsAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCulture
let inline (|EqualsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCultureIgnoreCase
let inline (|EqualsAnyChar|_|) (patterns: char[]) (str: string) =
    str |> equalsAnyChar patterns

let inline (|NotEqualsAny|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.Ordinal |> not
let inline (|NotEqualsAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.OrdinalIgnoreCase |> not
let inline (|NotEqualsAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCulture |> not
let inline (|NotEqualsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCultureIgnoreCase |> not
let inline (|NotEqualsAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCulture |> not
let inline (|NotEqualsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCultureIgnoreCase |> not
let inline (|NotEqualsAnyChar|_|) (patterns: char[]) (str: string) =
    str |> equalsAnyChar patterns |> not

let inline (|Contains|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.Ordinal))
let inline (|ContainsIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.OrdinalIgnoreCase))
let inline (|ContainsCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCulture))
let inline (|ContainsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCultureIgnoreCase))
let inline (|ContainsInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCulture))
let inline (|ContainsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCultureIgnoreCase))
let inline (|ContainsChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.Ordinal))

let inline (|NotContains|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.Ordinal) |> not)
let inline (|NotContainsIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.OrdinalIgnoreCase) |> not)
let inline (|NotContainsCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCulture) |> not)
let inline (|NotContainsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCultureIgnoreCase) |> not)
let inline (|NotContainsInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCulture) |> not)
let inline (|NotContainsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCultureIgnoreCase) |> not)
let inline (|NotContainsChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Contains(pattern) |> not)

let private containsAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <- str.Contains(pattern, comparison)
    found

let private containsAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str.Contains(pattern, comparison)
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private containsAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
        let pattern = enumerator.Current
        found <- str.Contains(pattern, comparison)
    found

let private containsAnyCharArray (patterns: char[]) (str: string) =
    if str.Length <> 1 then false else
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <- str.Contains(pattern)
    found

let private containsAnyCharList (patterns: char list) (str: string) =
    if str.Length <> 1 then false else
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str.Contains(pattern)
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private containsAnyCharSeq (patterns: char seq) (str: string) =
    if str.Length <> 1 then false else
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
         let pattern = enumerator.Current
         found <- str.Contains(pattern)
    found

let containsAny (patterns: string seq) (comparison: StringComparison) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (string[]) as patterns -> containsAnyArray patterns comparison str
        | :? (string list) as patterns -> containsAnyList patterns comparison str
        | _ -> containsAnySeq patterns comparison str

let containsAnyChar (patterns: char seq) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (char[]) as patterns -> containsAnyCharArray patterns str
        | :? (char list) as patterns -> containsAnyCharList patterns str
        | _ -> containsAnyCharSeq patterns str

let inline (|ContainsAny|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.Ordinal
let inline (|ContainsAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.OrdinalIgnoreCase
let inline (|ContainsAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.CurrentCulture
let inline (|ContainsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.CurrentCultureIgnoreCase
let inline (|ContainsAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.InvariantCulture
let inline (|ContainsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.InvariantCultureIgnoreCase
let inline (|ContainsAnyChar|_|) (patterns: char[]) (str: string) =
    str |> containsAnyChar patterns

let inline (|NotContainsAny|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.Ordinal |> not
let inline (|NotContainsAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.OrdinalIgnoreCase |> not
let inline (|NotContainsAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.CurrentCulture |> not
let inline (|NotContainsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.CurrentCultureIgnoreCase |> not
let inline (|NotContainsAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.InvariantCulture |> not
let inline (|NotContainsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.InvariantCultureIgnoreCase |> not
let inline (|NotContainsAnyChar|_|) (patterns: char[]) (str: string) =
 str |> containsAnyChar patterns |> not

let inline (|StartsWith|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.Ordinal))
let inline (|StartsWithIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.OrdinalIgnoreCase))
let inline (|StartsWithCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCulture))
let inline (|StartsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase))
let inline (|StartsWithInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCulture))
let inline (|StartsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase))
let inline (|StartsWithChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.StartsWith(pattern))

let inline (|NotStartsWith|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.Ordinal) |> not)
let inline (|NotStartsWithIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.OrdinalIgnoreCase) |> not)
let inline (|NotStartsWithCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCulture) |> not)
let inline (|NotStartsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> not)
let inline (|NotStartsWithInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCulture) |> not)
let inline (|NotStartsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> not)
let inline (|NotStartsWithChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.StartsWith(pattern) |> not)

let private startsWithAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <-  str.StartsWith(pattern, comparison)
    found

let private startsWithAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str.StartsWith(pattern, comparison)
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private startsWithAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
         let pattern = enumerator.Current
         found <- str.StartsWith(pattern, comparison)
    found

let private startsWithAnyCharArray (patterns: char[]) (str: string) =
    if str.Length <> 1 then false else
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <- str.StartsWith(pattern)
    found

let private startsWithAnyCharList (patterns: char list) (str: string) =
    if str.Length <> 1 then false else
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str.StartsWith(pattern)
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private startsWithAnyCharSeq (patterns: char seq) (str: string) =
    if str.Length <> 1 then false else
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
        let pattern = enumerator.Current
        found <- str.StartsWith(pattern)
    found

let startsWithAny (patterns: string seq) (comparison: StringComparison) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (string[]) as patterns -> startsWithAnyArray patterns comparison str
        | :? (string list) as patterns -> startsWithAnyList patterns comparison str
        | _ -> startsWithAnySeq patterns comparison str

let startsWithAnyChar (patterns: char seq) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (char[]) as patterns -> startsWithAnyCharArray patterns str
        | :? (char list) as patterns -> startsWithAnyCharList patterns str
        | _ -> startsWithAnyCharSeq patterns str

let inline (|StartsWithAny|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.Ordinal
let inline (|StartsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.OrdinalIgnoreCase
let inline (|StartsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCulture
let inline (|StartsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCultureIgnoreCase
let inline (|StartsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCulture
let inline (|StartsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCultureIgnoreCase
let inline (|StartsWithAnyChar|_|) (patterns: char[]) (str: string) =
    str |> startsWithAnyChar patterns

let inline (|NotStartsWithAny|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.Ordinal |> not
let inline (|NotStartsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.OrdinalIgnoreCase |> not
let inline (|NotStartsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCulture |> not
let inline (|NotStartsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> not
let inline (|NotStartsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCulture |> not
let inline (|NotStartsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> not
let inline (|NotStartsWithAnyChar|_|) (patterns: char[]) (str: string) =
    str |> startsWithAnyChar patterns |> not

let inline (|EndsWith|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.Ordinal))
let inline (|EndsWithIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.OrdinalIgnoreCase))
let inline (|EndsWithCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCulture))
let inline (|EndsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase))
let inline (|EndsWithInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCulture))
let inline (|EndsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCultureIgnoreCase))
let inline (|EndsWithChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.EndsWith(pattern))

let inline (|NotEndsWith|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.Ordinal) |> not)
let inline (|NotEndsWithIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.OrdinalIgnoreCase) |> not)
let inline (|NotEndsWithCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCulture) |> not)
let inline (|NotEndsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> not)
let inline (|NotEndsWithInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCulture) |> not)
let inline (|NotEndsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> not)
let inline (|NotEndsWithChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.EndsWith(pattern) |> not)

let private endsWithAnyArray (patterns: string[]) (comparison: StringComparison) (str: string) =
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <-  str.EndsWith(pattern, comparison)
    found

let private endsWithAnyList (patterns: string list) (comparison: StringComparison) (str: string) =
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str.EndsWith(pattern, comparison)
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private endsWithAnySeq (patterns: string seq) (comparison: StringComparison) (str: string) =
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
         let pattern = enumerator.Current
         found <- str.EndsWith(pattern, comparison)
    found

let private endsWithAnyCharArray (patterns: char[]) (str: string) =
    if str.Length <> 1 then false else
    let mutable i = 0
    let mutable found = false
    while not found && i < patterns.Length do
        let pattern = patterns[i]
        found <- str.EndsWith(pattern)
    found

let private endsWithAnyCharList (patterns: char list) (str: string) =
    if str.Length <> 1 then false else
    let mutable list = patterns
    let mutable found = false
    while not found &&
        (match list with
         | pattern :: rest ->
             found <- str.EndsWith(pattern)
             list <- rest
             true
         | [] ->
             false) do ()
    found

let private endsWithAnyCharSeq (patterns: char seq) (str: string) =
    if str.Length <> 1 then false else
    use enumerator = patterns.GetEnumerator()
    let mutable found = false
    while not found && enumerator.MoveNext() do
         let pattern = enumerator.Current
         found <- str.EndsWith(pattern)
    found

let endsWithAny (patterns: string seq) (comparison: StringComparison) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (string[]) as patterns -> endsWithAnyArray patterns comparison str
        | :? (string list) as patterns -> endsWithAnyList patterns comparison str
        | _ -> endsWithAnySeq patterns comparison str

let endsWithAnyChar (patterns: char seq) (str: string) =
    isNotNull str
    &&  match patterns with
        | :? (char[]) as patterns -> endsWithAnyCharArray patterns str
        | :? (char list) as patterns -> endsWithAnyCharList patterns str
        | _ -> endsWithAnyCharSeq patterns str

let inline (|EndsWithAny|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.Ordinal
let inline (|EndsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.OrdinalIgnoreCase
let inline (|EndsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCulture
let inline (|EndsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCultureIgnoreCase
let inline (|EndsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCulture
let inline (|EndsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCultureIgnoreCase
let inline (|EndsWithAnyChar|_|) (patterns: char[]) (str: string) =
    str |> endsWithAnyChar patterns

let inline (|NotEndsWithAny|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.Ordinal |> not
let inline (|NotEndsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.OrdinalIgnoreCase |> not
let inline (|NotEndsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCulture |> not
let inline (|NotEndsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> not
let inline (|NotEndsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCulture |> not
let inline (|NotEndsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> not
let inline (|NotEndsWithAnyChar|_|) (patterns: char[]) (str: string) =
    str |> endsWithAnyChar patterns |> not