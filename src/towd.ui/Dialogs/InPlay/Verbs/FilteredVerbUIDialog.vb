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
                Function(x) getTableValue(_Prompt, x))
    End Sub

    Private Function getTableValue(prompt As String, grouping As IGrouping(Of String, IVerbType)) As Func(Of IUIDialog)
        If grouping.Count = 1 Then
            Return Function() As IUIDialog
                       Return New VerbDetailUIDialog(context, grouping.Single, False, MakeCopy)
                   End Function
        End If
        Return Function() New FilteredVerbCategoryUIDialog(context, grouping.Key.ToVerbCategoryDescriptor.Name, grouping.Key, verbTypeFilter, MakeCopy)
    End Function

    Private Function getTableKey(grouping As IGrouping(Of String, IVerbType)) As String
        If grouping.Count = 1 Then
            Return grouping.Single.Name
        End If
        Return $"{grouping.Key.ToVerbCategoryDescriptor.Name}..."
    End Function
    Public Function GetLinesAsync() As Task(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean))) Implements IUIDialog.GetLinesAsync
        Return Task.FromResult(Of IEnumerable(Of (Mood As String, Text As String, EndsLine As Boolean)))(Array.Empty(Of (String, String, Boolean)))
    End Function

    Public Function GetChoicesAsync() As Task(Of IEnumerable(Of String)) Implements IUIDialog.GetChoicesAsync
        Dim result As New List(Of String) From
                {
                NEVER_MIND_TEXT
                }
        result.AddRange(table.Keys.Order)
        Return Task.FromResult(Of IEnumerable(Of String))(result)
    End Function

    Private _Prompt As String

    Public Function GetPromptAsync() As Task(Of String) Implements IUIDialog.GetPromptAsync
        Return Task.FromResult(_Prompt)
    End Function

    Public Function Choose(choice As String) As Task(Of IUIDialog) Implements IUIDialog.Choose
        Dim nextDialog As Func(Of IUIDialog) = Nothing
        If table.TryGetValue(choice, nextDialog) Then
            Return Task.FromResult(nextDialog())
        End If
        Return Task.FromResult(cancelDialog())
    End Function

    Public Function MakeCopy() As Func(Of IUIDialog) Implements IUIDialog.MakeCopy
        Return (Function() New FilteredVerbUIDialog(context, _Prompt, verbTypeFilter, cancelDialog))
    End Function
End Class
