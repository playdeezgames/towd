Imports towd.business

Friend Class GameMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Const CONTINUE_TEXT = "Continue"
    Const SCUM_SAVE_GAME_TEXT = "Scum Save Game"
    Const SCUM_LOAD_GAME_TEXT = "Scum Load Game"
    Const SAVE_GAME_TEXT = "Save Game"
    Const ABANDON_GAME_TEXT = "Abandon Game"

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
    End Sub

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(Array.Empty(Of UIDialogLine))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({
                CONTINUE_TEXT,
                SCUM_SAVE_GAME_TEXT,
                SCUM_LOAD_GAME_TEXT,
                SAVE_GAME_TEXT,
                ABANDON_GAME_TEXT
                })
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Game Menu")
    End Function

    Public Async Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case CONTINUE_TEXT
                Return NeutralUIDialog.DetermineInPlayDialog(context)
            Case SCUM_LOAD_GAME_TEXT
                If Await context.LoadGame(SaveSlot.ScumSlot) Then
                    Return NeutralUIDialog.DetermineInPlayDialog(context)
                End If
                Return Me
            Case SCUM_SAVE_GAME_TEXT
                Await context.SaveGame(SaveSlot.ScumSlot)
                Return New MessageBoxUIDialog("Saved!", {New UIDialogLine(Mood.Normal, $"You saved to {SaveSlot.ScumSlot.ToSaveSlotDescriptor.DisplayName}", True)}, MakeCopy)
            Case SAVE_GAME_TEXT
                Return New SaveGameUIDialog(context, MakeCopy)
            Case ABANDON_GAME_TEXT
                Return New ConfirmUIDialog("Are you sure you want to abandon the game?", Function()
                                                                                             context.World.Abandon()
                                                                                             Return New MainMenuUIDialog(context)
                                                                                         End Function, MakeCopy)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New GameMenuUIDialog(context))
    End Function
End Class
