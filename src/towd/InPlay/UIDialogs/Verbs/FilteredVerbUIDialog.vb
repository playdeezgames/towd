Imports towd.business

Friend Class FilteredVerbUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private ReadOnly table As IReadOnlyDictionary(Of String, Func(Of IUIDialog))

    Public Sub New(context As IUIContext, prompt As String, verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean), cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.Prompt = prompt
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
                       Return New VerbDetailUIDialog(context, grouping.Single, Function() New FilteredVerbUIDialog(context, Prompt, verbTypeFilter, cancelDialog))
                   End Function
        End If
        Return Function() New FilteredVerbCategoryUIDialog(context, grouping.Key.ToVerbCategoryDescriptor.Name, grouping.Key, verbTypeFilter, Function() New FilteredVerbUIDialog(context, Prompt, verbTypeFilter, cancelDialog))
    End Function

    Private Function getTableKey(grouping As IGrouping(Of String, IVerbType)) As String
        If grouping.Count = 1 Then
            Return grouping.Single.Name
        End If
        Return $"{grouping.Key.ToVerbCategoryDescriptor.Name}..."
    End Function
    Public ReadOnly Property Lines As IEnumerable(Of String) Implements IUIDialog.Lines
        Get
            Return Array.Empty(Of String)
        End Get
    End Property

    Public ReadOnly Property Choices As IEnumerable(Of String) Implements IUIDialog.Choices
        Get
            Dim result As New List(Of String) From
                {
                NEVER_MIND_TEXT
                }
            result.AddRange(table.Keys.Order)
            Return result
        End Get
    End Property

    Public ReadOnly Property Prompt As String Implements IUIDialog.Prompt

    Public Function Choose(choice As String) As (String, IUIDialog) Implements IUIDialog.Choose
        Dim nextDialog As Func(Of IUIDialog) = Nothing
        If table.TryGetValue(choice, nextDialog) Then
            Return (Nothing, nextDialog())
        End If
        Return (Nothing, cancelDialog())
    End Function
End Class
