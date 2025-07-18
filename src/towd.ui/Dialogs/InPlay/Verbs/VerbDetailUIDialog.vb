﻿Imports towd.business

Public Class VerbDetailUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly verbType As IVerbType
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly performAgain As Boolean
    Const NEVER_MIND_TEXT = "Never Mind"
    Const PERFORM_TEXT = "Perform"
    Const PERFORM_AGAIN_TEXT = "Perform Again"

    Sub New(
           context As IUIContext(Of IWorld),
           verbType As IVerbType,
           performAgain As Boolean,
           cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.verbType = verbType
        Me.cancelDialog = cancelDialog
        Me.performAgain = performAgain
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return {(Mood.Normal, verbType.Description, True)}
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String)
            If performAgain AndAlso verbType.CanPerform(context.World.Avatar) Then
                result.Add(PERFORM_AGAIN_TEXT)
            End If
            result.Add(NEVER_MIND_TEXT)
            If Not performAgain AndAlso verbType.CanPerform(context.World.Avatar) Then
                result.Add(PERFORM_TEXT)
            End If
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return verbType.Name
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Select Case choice
            Case PERFORM_TEXT, PERFORM_AGAIN_TEXT
                verbType.Perform(context.World.Avatar)
                Return MessageUIDialog.DetermineMessageDialog(context, Function() New VerbDetailUIDialog(context, verbType, True, cancelDialog))
            Case NEVER_MIND_TEXT
                Return cancelDialog()
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
