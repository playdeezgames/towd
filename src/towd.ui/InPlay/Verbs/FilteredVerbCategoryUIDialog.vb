Imports towd.business

Public Class FilteredVerbCategoryUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext
    Private ReadOnly verbCategoryType As String
    Private ReadOnly verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private ReadOnly table As IReadOnlyDictionary(Of String, Func(Of IUIDialog))

    Sub New(
           context As IUIContext,
           prompt As String,
           verbCategoryType As String,
           verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean),
           cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me.Prompt = prompt
        Me.verbCategoryType = verbCategoryType
        Me.verbTypeFilter = verbTypeFilter
        Me.cancelDialog = cancelDialog
        table = VerbTypes.Descriptors.Values.
            Where(Function(x) x.VerbCategoryType = verbCategoryType AndAlso verbTypeFilter(x, context.World.Avatar)).
            ToDictionary(
                AddressOf getTableKey,
                AddressOf getTableValue)
    End Sub

    Private Function getTableValue(verbType As IVerbType) As Func(Of IUIDialog)
        Return Function()
                   Return New VerbDetailUIDialog(
                        context,
                        verbType,
                        Function() New FilteredVerbCategoryUIDialog(context, Prompt, verbCategoryType, verbTypeFilter, cancelDialog))
               End Function
    End Function

    Private Function getTableKey(verbType As IVerbType) As String
        Return verbType.Name
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

    Public Function Choose(choice As String) As IUIDialog Implements IUIDialog.Choose
        Dim nextDialog As Func(Of IUIDialog) = Nothing
        If table.TryGetValue(choice, nextDialog) Then
            Return nextDialog()
        End If
        Return cancelDialog()
    End Function
End Class
