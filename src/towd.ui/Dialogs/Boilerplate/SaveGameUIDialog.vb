Imports towd.business

Friend Class SaveGameUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly table As IReadOnlyDictionary(Of String, ISaveSlot)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext(Of IWorld), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table = SaveSlots.Descriptors.ToDictionary(Function(x)
                                                       Dim saveExists = context.Persister.SaveExists(x.Value)
                                                       If saveExists.HasValue Then
                                                           Return $"{x.Value.DisplayName}(Last saved: {saveExists.Value})"
                                                       End If
                                                       Return x.Value.DisplayName
                                                   End Function, Function(x) x.Value)
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return Array.Empty(Of (String, String, Boolean))
    End Function

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From
                {
                    NEVER_MIND_TEXT
                }
            result.AddRange(table.Keys)
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Save Menu"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim saveSlot As ISaveSlot = Nothing
        If table.TryGetValue(choice, saveSlot) Then
            If context.Persister.SaveExists(saveSlot).HasValue Then
                Return New ConfirmUIDialog(
                    $"Are you sure you want to overwrite {saveSlot.DisplayName}",
                    Function() SaveGame(saveSlot),
                    Function() Me)
            End If
            Return SaveGame(saveSlot)
        End If
        Return cancelDialog()
    End Function

    Private Function SaveGame(saveSlot As ISaveSlot) As IUIDialog
        context.SaveGame(saveSlot.SaveSlot)
        Return New MessageBoxUIDialog("Game Saved!", {(Mood.Normal, $"Saved game in {saveSlot.DisplayName}", True)}, cancelDialog)
    End Function
End Class
