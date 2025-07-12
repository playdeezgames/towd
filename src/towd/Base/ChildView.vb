Imports towd.business
Imports towd.data

Friend MustInherit Class ChildView
    Inherits View
    Private ReadOnly mainView As MainView
    Protected Shared ReadOnly Context As IContext = New Context
    Protected Shared Function LoadGame(saveSlot As String) As Boolean
        If Context.LoadGame(saveSlot) Then
            MessageBox.Query("Game Loaded!", $"{saveSlot.ToSaveSlotDescriptor.DisplayName} is loaded!", "Ok")
            Return True
        End If
        MessageBox.ErrorQuery("Load Failed!", $"Could not load {saveSlot.ToSaveSlotDescriptor.DisplayName}!", "Ok")
        Return False
    End Function
    Protected Shared Sub SaveGame(saveSlot As String, notify As Boolean)
        Context.SaveGame(saveSlot, Sub() If notify Then MessageBox.Query("Game Saved!", $"{saveSlot.ToSaveSlotDescriptor.DisplayName} is saved!", "Ok"))
    End Sub
    Sub New(mainView As MainView)
        Me.mainView = mainView
        Width = [Dim].Fill()
        Height = [Dim].Fill()
        AddHandler Me.KeyPress, AddressOf OnKeyPress
    End Sub

    Protected Overridable Sub OnKeyPress(args As KeyEventEventArgs)
        'nada
    End Sub
    Friend Overridable Sub UpdateView()
        Dim character = Context.World?.Avatar
        While character?.HasMessages
            MessageBox.Query("", String.Join(vbCrLf, character.CurrentMessage), "Ok")
            character.DismissMessage()
        End While
    End Sub
    Protected Sub ShowState(gameState As String)
        mainView.ShowState(gameState)
    End Sub
End Class
