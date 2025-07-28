Imports towd.business

Friend Class MainMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Const EMBARK_TEXT = "Embark!"
    Const SCUM_LOAD_TEXT = "Scum Load"
    Const LOAD_TEXT = "Load..."
    Const QUIT_TEXT = "Quit"

    Public Sub New(context As IUIContext(Of IWorld))
        Me.context = context
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(Array.Empty(Of UIDialogLine))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({
                EMBARK_TEXT,
                SCUM_LOAD_TEXT,
                LOAD_TEXT,
                QUIT_TEXT
                })
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Main Menu:")
    End Function

    Public Async Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case EMBARK_TEXT
                context.World.Initialize()
                Return NeutralUIDialog.DetermineInPlayDialog(context)
            Case SCUM_LOAD_TEXT
                If Await context.LoadGame(SaveSlot.ScumSlot) Then
                    Return New MessageBoxUIDialog("Load Success!", {New UIDialogLine(Mood.Normal, $"You loaded {SaveSlot.ScumSlot.ToSaveSlotDescriptor.DisplayName}!", True)}, Function() NeutralUIDialog.DetermineInPlayDialog(context))
                End If
                Return New MessageBoxUIDialog("Load Failed!", {New UIDialogLine(Mood.Normal, $"Failed to load {SaveSlot.ScumSlot.ToSaveSlotDescriptor.DisplayName}!", True)}, MakeCopy)
            Case LOAD_TEXT
                Return New LoadMenuUIDialog(context, MakeCopy)
            Case QUIT_TEXT
                Return New ConfirmUIDialog("Are you sure you want to quit?", Function() Nothing, MakeCopy)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New MainMenuUIDialog(context))
    End Function
End Class
