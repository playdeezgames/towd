Friend Class LoadMenuUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private table As New Dictionary(Of String, ISaveSlot)

    Public Sub New(context As IUIContext, cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        table.Add(NEVER_MIND_TEXT, Nothing)
        For Each saveSlot In SaveSlots.Descriptors.Values.Where(Function(x) x.SaveExists)
            table.Add(saveSlot.ToString(), saveSlot)
        Next
    End Sub

    Public ReadOnly Property Lines As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of (String, String, Boolean))
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Return table.Keys
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
        Get
            Return "Load Menu"
        End Get
    End Property

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim saveSlot As ISaveSlot = table(choice)
        If saveSlot Is Nothing Then
            Return cancelDialog()
        End If
        If context.LoadGame(saveSlot.SaveSlot) Then
            Return New MessageBoxUIDialog("Load Success!", {(Mood.Normal, $"You loaded {saveSlot.DisplayName}!", True)}, Function() NeutralUIDialog.DetermineInPlayDialog(context))
        Else
            Return New MessageBoxUIDialog("Load Failed!", {(Mood.Normal, $"Failed to load {saveSlot.DisplayName}!", True)}, cancelDialog)
        End If
        Throw New NotImplementedException
    End Function
End Class
