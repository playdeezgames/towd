﻿Imports towd.business

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

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of (String, String, Boolean))
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return {
                CONTINUE_TEXT,
                SCUM_SAVE_GAME_TEXT,
                SCUM_LOAD_GAME_TEXT,
                SAVE_GAME_TEXT,
                ABANDON_GAME_TEXT
                }
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Game Menu"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Select Case choice
            Case CONTINUE_TEXT
                Return NeutralUIDialog.DetermineInPlayDialog(context)
            Case SCUM_LOAD_GAME_TEXT
                If context.LoadGame(SaveSlot.ScumSlot) Then
                    Return NeutralUIDialog.DetermineInPlayDialog(context)
                End If
                Return Me
            Case SCUM_SAVE_GAME_TEXT
                context.SaveGame(SaveSlot.ScumSlot)
                Return Me
            Case SAVE_GAME_TEXT
                Return New SaveGameUIDialog(context, Function() Me)
            Case ABANDON_GAME_TEXT
                Return New ConfirmUIDialog("Are you sure you want to abandon the game?", Function()
                                                                                             context.World.Abandon()
                                                                                             Return New MainMenuUIDialog(context)
                                                                                         End Function, Function() Me)
            Case Else
                Throw New NotImplementedException
        End Select
    End Function
End Class
