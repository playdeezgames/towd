Imports towd.business

Friend Class RenameLocationUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly nextDialog As Func(Of IUIDialog)
    Const CONFIRM_TEXT = "Confirm"
    Const CANCEL_TEXT = "Cancel"
    Const NEW_NAME_PARAMETER = "New Name"

    Public Sub New(context As IUIContext(Of IWorld), nextDialog As Func(Of IUIDialog))
        Me.context = context
        Me.nextDialog = nextDialog
    End Sub

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Dim result As New Dictionary(Of String, String) From
            {{NEW_NAME_PARAMETER, context.World.Avatar.CurrentLocation.Name}}
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(result)
    End Function

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Array.Empty(Of UIDialogLine).AsEnumerable)
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult({CANCEL_TEXT, CONFIRM_TEXT}.AsEnumerable)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("(Re)Name Location")
    End Function

    Public Function Choose(choice As String, parameters As IReadOnlyDictionary(Of String, String)) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case CONFIRM_TEXT
                context.World.Avatar.CurrentLocation.Name = parameters(NEW_NAME_PARAMETER)
                Return Task.FromResult(nextDialog())
            Case CANCEL_TEXT
                Return Task.FromResult(nextDialog())
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return Function() New RenameLocationUIDialog(context, nextDialog)
    End Function
End Class
