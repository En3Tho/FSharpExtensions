// auto-generated
[<Microsoft.FSharp.Core.AutoOpen>]
module Microsoft.Extensions.Logging.ILoggerExtensions

open System

type ILogger with
    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogTrace(eventId, exn, message, args)

    member inline this.LogTrace(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogTrace(eventId, exn, message, args)
    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogTrace(eventId, message, args)

    member inline this.LogTrace(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogTrace(eventId, message, args)
    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogTrace(exn, message, args)

    member inline this.LogTrace(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogTrace(exn, message, args)
    member inline this.LogTrace(message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogTrace(message, args)

    member inline this.LogTrace(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Trace) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogTrace(message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogDebug(eventId, exn, message, args)

    member inline this.LogDebug(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogDebug(eventId, exn, message, args)
    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogDebug(eventId, message, args)

    member inline this.LogDebug(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogDebug(eventId, message, args)
    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogDebug(exn, message, args)

    member inline this.LogDebug(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogDebug(exn, message, args)
    member inline this.LogDebug(message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogDebug(message, args)

    member inline this.LogDebug(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Debug) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogDebug(message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogInformation(eventId, exn, message, args)

    member inline this.LogInformation(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogInformation(eventId, exn, message, args)
    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogInformation(eventId, message, args)

    member inline this.LogInformation(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogInformation(eventId, message, args)
    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogInformation(exn, message, args)

    member inline this.LogInformation(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogInformation(exn, message, args)
    member inline this.LogInformation(message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogInformation(message, args)

    member inline this.LogInformation(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Information) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogInformation(message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogWarning(eventId, exn, message, args)

    member inline this.LogWarning(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogWarning(eventId, exn, message, args)
    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogWarning(eventId, message, args)

    member inline this.LogWarning(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogWarning(eventId, message, args)
    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogWarning(exn, message, args)

    member inline this.LogWarning(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogWarning(exn, message, args)
    member inline this.LogWarning(message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogWarning(message, args)

    member inline this.LogWarning(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Warning) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogWarning(message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogError(eventId, exn, message, args)

    member inline this.LogError(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogError(eventId, exn, message, args)
    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogError(eventId, message, args)

    member inline this.LogError(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogError(eventId, message, args)
    member inline this.LogError(exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogError(exn, message, args)

    member inline this.LogError(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogError(exn, message, args)
    member inline this.LogError(message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogError(message, args)

    member inline this.LogError(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Error) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogError(message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogCritical(eventId, exn, message, args)

    member inline this.LogCritical(eventId: EventId, exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogCritical(eventId, exn, message, args)
    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogCritical(eventId, message, args)

    member inline this.LogCritical(eventId: EventId, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogCritical(eventId, message, args)
    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogCritical(exn, message, args)

    member inline this.LogCritical(exn: Exception, message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogCritical(exn, message, args)
    member inline this.LogCritical(message: String, arg0: 'a0) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14 |]
            this.LogCritical(message, args)

    member inline this.LogCritical(message: String, arg0: 'a0, arg1: 'a1, arg2: 'a2, arg3: 'a3, arg4: 'a4, arg5: 'a5, arg6: 'a6, arg7: 'a7, arg8: 'a8, arg9: 'a9, arg10: 'a10, arg11: 'a11, arg12: 'a12, arg13: 'a13, arg14: 'a14, arg15: 'a15) =
        if this.IsEnabled(LogLevel.Critical) then
            let args: obj[] = [| arg0; arg1; arg2; arg3; arg4; arg5; arg6; arg7; arg8; arg9; arg10; arg11; arg12; arg13; arg14; arg15 |]
            this.LogCritical(message, args)