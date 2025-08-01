﻿Imports towd.business

Friend Class MessageUIDialog
    Implements IUIDialog

    Friend Shared Function DetermineMessageDialog(context As IUIContext(Of IWorld), nextDialog As Func(Of IUIDialog)) As IUIDialog
        If context.World.Avatar?.HasMessages Then
            Return New MessageUIDialog(context, nextDialog)
        End If
        Return nextDialog()
    End Function

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly nextDialog As Func(Of IUIDialog)

    Public Sub New(context As IUIContext(Of IWorld), nextDialog As Func(Of IUIDialog))
        Me.context = context
        Me.nextDialog = nextDialog
    End Sub

    Public Function GetLinesAsync() As Task(Of IEnumerable(Of UIDialogLine)) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of UIDialogLine))(context.World.Avatar.CurrentMessage.Select(Function(x) New UIDialogLine(Mood.Normal, x, True)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Return Task.FromResult(Of IEnumerable(Of String))({"Ok"})
    End Function

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(String.Empty)
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        context.World.Avatar.DismissMessage()
        If context.World.Avatar.HasMessages Then
            Return Task.FromResult(Of IUIDialog)(Me)
        End If
        Return Task.FromResult(nextDialog())
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New MessageUIDialog(context, nextDialog))
    End Function
End Class
