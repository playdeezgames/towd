Imports System.IO
Imports System.Text.Json

Friend Class SaveMenuState
    Inherits ChildView
    Private ReadOnly saveSlotListView As ListView

    Public Sub New(mainView As MainView)
        MyBase.New(mainView)
        Dim titleLabel As New Label With
            {
                .Width = [Dim].Fill,
                .Text = "Save Menu (Esc to cancel)",
                .TextAlignment = TextAlignment.Centered
            }
        Add(titleLabel)
        saveSlotListView = New ListView With
            {
                .Y = Pos.Bottom(titleLabel),
                .Width = [Dim].Fill,
                .Height = [Dim].Fill
            }
        saveSlotListView.SetSource(SaveSlots.Descriptors.Values.ToList)
        AddHandler saveSlotListView.OpenSelectedItem, AddressOf OnSaveSlotListViewOpenSelectedItem
        Add(saveSlotListView)
    End Sub

    Private Sub OnSaveSlotListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim listItem = CType(args.Value, ISaveSlot)
        If File.Exists(listItem.Filename) Then
            If MessageBox.Query("Confirm Overwrite?", "Are you sure you want to overwrite the save slot?", "No", "Yes") = 0 Then
                Return
            End If
        End If
        listItem.SaveGame(World)
    End Sub

    Friend Overrides Sub UpdateView()
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            ShowState(GameState.GameMenu)
        End If
    End Sub
End Class
