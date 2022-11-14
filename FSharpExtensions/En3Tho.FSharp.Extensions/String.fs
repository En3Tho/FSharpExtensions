module En3Tho.FSharp.Extensions.String

open System

let inline nullOrWhiteSpaceCheck argName arg = if String.IsNullOrWhiteSpace arg then nullArg argName |> ignore
let inline nullOrEmptyCheck argName arg = if String.IsNullOrEmpty arg then nullArg argName |> ignore
let inline ensureNotNullOrWhiteSpace argName arg = if String.IsNullOrWhiteSpace arg then nullArg argName else arg
let inline ensureNotNullOrEmpty argName arg = if String.IsNullOrEmpty arg then nullArg argName else arg
let inline defaultValue def str = if String.IsNullOrEmpty str then def else str
let inline defaultValueW def str = if String.IsNullOrWhiteSpace str then def else str
let inline defaultWith ([<InlineIfLambda>] defThunk) str = if String.IsNullOrEmpty str then defThunk() else str
let inline defaultWithW ([<InlineIfLambda>] defThunk) str = if String.IsNullOrWhiteSpace str then defThunk() else str
let inline truncate maxLength (str: string) = if str.Length <= maxLength then str else str.Substring(0, maxLength)

let [<return: Struct>] inline (|NullOrEmpty|_|) (str: string) =
    String.IsNullOrEmpty(str) |> ValueOption.ofBool
let [<return: Struct>] inline (|NotNullOrEmpty|_|) (str: string) =
    String.IsNullOrEmpty(str) |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|NullOrWhiteSpace|_|) (str: string) =
    String.IsNullOrWhiteSpace(str) |> ValueOption.ofBool
let [<return: Struct>] inline (|NotNullOrWhiteSpace|_|) (str: string) =
    String.IsNullOrWhiteSpace(str) |> not |> ValueOption.ofBool

let [<return: Struct>] inline (|Equals|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.Ordinal)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.OrdinalIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Length = 1 && str[0] = pattern) |> ValueOption.ofBool

let [<return: Struct>] inline (|EqualsNot|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.Ordinal) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsNotIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.OrdinalIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsNotCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.CurrentCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsNotInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Equals(pattern, StringComparison.InvariantCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsNotChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Length = 1 && str[0] = pattern |> not) |> ValueOption.ofBool

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

let [<return: Struct>] inline (|EqualsAny|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.Ordinal |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyChar|_|) (patterns: char[]) (str: string) =
    str |> equalsAnyChar patterns |> ValueOption.ofBool

let [<return: Struct>] inline (|EqualsAnyNot|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> equalsAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EqualsAnyNotChar|_|) (patterns: char[]) (str: string) =
    str |> equalsAnyChar patterns |> not |> ValueOption.ofBool

let [<return: Struct>] inline (|Contains|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.Ordinal)) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.OrdinalIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.Ordinal)) |> ValueOption.ofBool

let [<return: Struct>] inline (|ContainsNot|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.Ordinal) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsNotIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.OrdinalIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsNotCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.CurrentCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsNotInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.Contains(pattern, StringComparison.InvariantCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsNotChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.Contains(pattern) |> not) |> ValueOption.ofBool

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

let [<return: Struct>] inline (|ContainsAny|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.Ordinal |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> containsAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyChar|_|) (patterns: char[]) (str: string) =
    str |> containsAnyChar patterns |> ValueOption.ofBool

let [<return: Struct>] inline (|ContainsAnyNot|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
 str |> containsAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|ContainsAnyNotChar|_|) (patterns: char[]) (str: string) =
 str |> containsAnyChar patterns |> not |> ValueOption.ofBool

let [<return: Struct>] inline (|StartsWith|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.Ordinal)) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.OrdinalIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.StartsWith(pattern)) |> ValueOption.ofBool

let [<return: Struct>] inline (|StartsWithNot|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.Ordinal) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithNotIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.OrdinalIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithNotCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithNotInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.StartsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithNotChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.StartsWith(pattern) |> not) |> ValueOption.ofBool

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

let [<return: Struct>] inline (|StartsWithAny|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.Ordinal |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyChar|_|) (patterns: char[]) (str: string) =
    str |> startsWithAnyChar patterns |> ValueOption.ofBool

let [<return: Struct>] inline (|StartsWithAnyNot|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> startsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|StartsWithAnyNotChar|_|) (patterns: char[]) (str: string) =
    str |> startsWithAnyChar patterns |> not |> ValueOption.ofBool

let [<return: Struct>] inline (|EndsWith|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.Ordinal)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.OrdinalIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCulture)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCultureIgnoreCase)) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.EndsWith(pattern)) |> ValueOption.ofBool

let [<return: Struct>] inline (|EndsWithNot|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.Ordinal) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithNotIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.OrdinalIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithNotCurrentCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithNotCurrentCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.CurrentCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithNotInvariantCulture|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCulture) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithNotInvariantCultureIgnoreCase|_|) (pattern: string) (str: string) =
    (isNotNull str && str.EndsWith(pattern, StringComparison.InvariantCultureIgnoreCase) |> not) |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithNotChar|_|) (pattern: char) (str: string) =
    (isNotNull str && str.EndsWith(pattern) |> not) |> ValueOption.ofBool

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

let [<return: Struct>] inline (|EndsWithAny|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.Ordinal |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.OrdinalIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCulture |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyChar|_|) (patterns: char[]) (str: string) =
    str |> endsWithAnyChar patterns |> ValueOption.ofBool

let [<return: Struct>] inline (|EndsWithAnyNot|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.Ordinal |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyNotIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.OrdinalIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyNotCurrentCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyNotCurrentCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.CurrentCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyNotInvariantCulture|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCulture |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyNotInvariantCultureIgnoreCase|_|) (patterns: string seq) (str: string) =
    str |> endsWithAny patterns StringComparison.InvariantCultureIgnoreCase |> not |> ValueOption.ofBool
let [<return: Struct>] inline (|EndsWithAnyNotChar|_|) (patterns: char[]) (str: string) =
    str |> endsWithAnyChar patterns |> not |> ValueOption.ofBool