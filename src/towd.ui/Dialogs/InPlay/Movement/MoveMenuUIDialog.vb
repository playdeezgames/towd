﻿Imports towd.business

Friend Class MoveMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private table As New Dictionary(Of String, IDirection)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table.Add(NEVER_MIND_TEXT, Nothing)
        For Each descriptor In context.World.Avatar.CurrentLocation.Neighbors.Select(Function(x) x.Key.ToDirectionDescriptor)
            table.Add(descriptor.Name, descriptor)
        Next
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return {(Mood.Normal, "Choose a direction.", True)}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return table.Keys
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Which Way?"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim descriptor = table(choice)
        If descriptor Is Nothing Then
            Return cancelDialog()
        End If
        context.World.Avatar.Move(descriptor.Direction)
        context.World.AdvanceTime(1)
        Return NeutralUIDialog.DetermineInPlayDialog(context)
    End Function
End Class
