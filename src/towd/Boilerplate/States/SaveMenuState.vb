Imports System.IO
Imports System.Text.Json
Imports System.Threading.Channels

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
                .Height = [Dim].Fill - 3
            }
        saveSlotListView.SetSource(SaveSlots.Descriptors.Values.ToList)
        AddHandler saveSlotListView.OpenSelectedItem, AddressOf OnSaveSlotListViewOpenSelectedItem
        Add(saveSlotListView)
        Dim closeButton As New Button("Close") With
            {
                .X = Pos.Center,
                .Y = Pos.Bottom(saveSlotListView) + 1
            }
        AddHandler closeButton.Clicked, AddressOf OnCloseButtonClicked
        Add(closeButton)
    End Sub

    Private Sub OnCloseButtonClicked()
        ShowState(Nothing, New GameMenuUIDialog(Context))
    End Sub

    Private Sub OnSaveSlotListViewOpenSelectedItem(args As ListViewItemEventArgs)
        Dim listItem = CType(args.Value, ISaveSlot)
        If listItem.SaveExists Then
            If MessageBox.Query("Confirm Overwrite?", $"Are you sure you want to overwrite {listItem.DisplayName}?", "No", "Yes") = 0 Then
                Return
            End If
        End If
        SaveGame(listItem.SaveSlot, True)
        UpdateView()
    End Sub

    Friend Overrides Sub UpdateView()
        MyBase.UpdateView()
    End Sub

    Protected Overrides Sub OnKeyPress(args As KeyEventEventArgs)
        If args.KeyEvent.Key = Key.Esc Then
            args.Handled = True
            ShowState(Nothing, New GameMenuUIDialog(Context))
        End If
    End Sub
End Class
