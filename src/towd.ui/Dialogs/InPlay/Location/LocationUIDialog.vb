Imports towd.business

Friend Class LocationUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NAME_TEXT = "Name..."
    Const RENAME_TEXT = "Rename..."
    Const NEVERMIND_TEXT = "Never Mind"

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Dim result As New List(Of UIDialogLine)
        Dim location = context.World.Avatar.CurrentLocation
        result.AddRange(location.Description.Select(Function(x) New UIDialogLine(Mood.Normal, x, True)))
        Return Task.FromResult(Of
            IEnumerable(Of
                UIDialogLine))(result)
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From {
            NEVERMIND_TEXT
        }
        Dim location = context.World.Avatar.CurrentLocation
        If location.HasName Then
            result.Add(RENAME_TEXT)
        Else
            result.Add(NAME_TEXT)
        End If
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Location Details")
    End Function

    Public Function Choose(choice As String, parameters As IReadOnlyDictionary(Of String, String)) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Select Case choice
            Case NEVERMIND_TEXT
                Return Task.FromResult(Of IUIDialog)(cancelDialog())
            Case NAME_TEXT, RENAME_TEXT
                Return Task.FromResult(Of IUIDialog)(New RenameLocationUIDialog(context, MakeCopy))
            Case Else
                Throw New NotImplementedException
        End Select
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return Function() New LocationUIDialog(context, cancelDialog)
    End Function
End Class
