Imports Spectre.Console
Imports towd.ui

Module Program
    Sub Main(args As String())
        Dim context As IUIContext = New UIContext
        context.Dialog = New SplashUIDialog(context)
        While context.Dialog IsNot Nothing
            AnsiConsole.Clear()
            For Each line In context.Dialog.Lines
                AnsiConsole.WriteLine(line)
            Next
            Dim prompt As New SelectionPrompt(Of String) With {.Title = context.Dialog.Prompt}
            prompt.AddChoices(context.Dialog.Choices.ToArray())
            Dim choice = AnsiConsole.Prompt(prompt)
            context.Dialog = context.Dialog.Choose(choice)
        End While
    End Sub
End Module
