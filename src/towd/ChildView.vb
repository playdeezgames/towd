﻿Imports towd.business
Imports towd.data

Friend MustInherit Class ChildView
    Inherits View
    Private ReadOnly mainView As MainView
    Private Shared worldData As New WorldData
    Protected Shared ReadOnly Property World As IWorld
        Get
            Return New World(worldData)
        End Get
    End Property
    Sub New(mainView As MainView)
        Me.mainView = mainView
        Width = [Dim].Fill()
        Height = [Dim].Fill()
        AddHandler Me.KeyPress, AddressOf OnKeyPress


    End Sub

    Protected Overridable Sub OnKeyPress(args As KeyEventEventArgs)
        'nada
    End Sub
    Friend MustOverride Sub UpdateView()
    Protected Sub ShowState(gameState As GameState)
        mainView.ShowState(gameState)
    End Sub
End Class
