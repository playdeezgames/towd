Imports towd.business

Friend Class FilteredVerbUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private ReadOnly table As IReadOnlyDictionary(Of String, Func(Of IUIDialog))

    Public Sub New(context As IUIContext(Of IWorld), prompt As String, verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me._Prompt = prompt
        Me.verbTypeFilter = verbTypeFilter
        Me.cancelDialog = cancelDialog
        table = VerbTypes.Descriptors.Values.
            Where(Function(x) verbTypeFilter(x, context.World.Avatar)).
            GroupBy(Function(x) x.VerbCategoryType).
            ToDictionary(
                AddressOf getTableKey,
                AddressOf getTableValue)
    End Sub

    Private Function getTableValue(grouping As IGrouping(Of String, IVerbType)) As Func(Of IUIDialog)
        If grouping.Count = 1 Then
            Return Function() As IUIDialog
                       Return New VerbDetailUIDialog(context, grouping.Single, False, Function() New FilteredVerbUIDialog(context, GetPrompt(), verbTypeFilter, cancelDialog))
                   End Function
        End If
        Return Function() New FilteredVerbCategoryUIDialog(context, grouping.Key.ToVerbCategoryDescriptor.Name, grouping.Key, verbTypeFilter, Function() New FilteredVerbUIDialog(context, GetPrompt(), verbTypeFilter, cancelDialog))
    End Function

    Private Function getTableKey(grouping As IGrouping(Of String, IVerbType)) As String
        If grouping.Count = 1 Then
            Return grouping.Single.Name
        End If
        Return $"{grouping.Key.ToVerbCategoryDescriptor.Name}..."
    End Function
    Public Function GetLines() As IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)) Implements IUIDialog.GetLines
        Return Array.Empty(Of (String, String, Boolean))
    End Function

    Public Function GetChoices() As IEnumerable(Of String) Implements IUIDialog.GetChoices
        Dim result As New List(Of String) From
                {
                NEVER_MIND_TEXT
                }
        result.AddRange(table.Keys.Order)
        Return result
    End Function

    Private _Prompt As String

    Public Function GetPrompt() As String Implements IUIDialog.GetPrompt
        Return _Prompt
    End Function

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim nextDialog As Func(Of IUIDialog) = Nothing
        If table.TryGetValue(choice, nextDialog) Then
            Return nextDialog()
        End If
        Return cancelDialog()
    End Function
End Class
