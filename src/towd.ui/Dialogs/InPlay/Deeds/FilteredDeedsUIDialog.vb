Imports towd.business

Friend Class FilteredDeedsUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Private ReadOnly table As IReadOnlyDictionary(Of String, IDeed)
    Const NEVER_MIND_TEXT = "Never Mind"

    Public Sub New(context As IUIContext(Of IWorld), prompt As String, deedFilter As Func(Of IDeed, Boolean), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.cancelDialog = cancelDialog
        Me.Prompt = prompt
        Me.deedFilter = deedFilter
        table = Deeds.Descriptors.Where(Function(x) deedFilter(x.Value)).ToDictionary(Function(x) x.Value.Name, Function(x) x.Value)
    End Sub

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
        Return Array.Empty(Of (String, String, Boolean))
    End Function

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From {
                    NEVER_MIND_TEXT
                }
            result.AddRange(table.Keys.OrderBy(Function(x) x))
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt
    Private ReadOnly deedFilter As Func(Of IDeed, Boolean)

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim deed As IDeed = Nothing
        If table.TryGetValue(choice, deed) Then
            Return New DeedDetailUIDialog(deed, Function() New FilteredDeedsUIDialog(context, Prompt, deedFilter, cancelDialog))
        End If
        Return cancelDialog()
    End Function
End Class
