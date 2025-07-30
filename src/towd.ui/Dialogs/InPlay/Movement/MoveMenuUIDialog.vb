Imports towd.business

Friend Class MoveMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private table As New Dictionary(Of String, IDirection)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Function GetParametersAsync() As Task(Of IReadOnlyDictionary(Of String, String)) Implements IUIDialog.GetParametersAsync
        Return Task.FromResult(Of IReadOnlyDictionary(Of String, String))(Nothing)
    End Function

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table.Add(NEVER_MIND_TEXT, Nothing)
        For Each descriptor In context.World.Avatar.CurrentLocation.Neighbors.Select(Function(x) x.Key.ToDirectionDescriptor)
            table.Add(descriptor.Name, descriptor)
        Next
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))({New UIDialogLine(Mood.Normal, "Choose a direction.", True)})
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))(table.Keys)
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult("Which Way?")
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim descriptor = table(choice)
        If descriptor Is Nothing Then
            Return Task.FromResult(cancelDialog())
        End If
        context.World.Avatar.Move(descriptor.Direction)
        context.World.AdvanceTime(1)
        Return Task.FromResult(NeutralUIDialog.DetermineInPlayDialog(context))
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New MoveMenuUIDialog(context, cancelDialog))
    End Function
End Class
