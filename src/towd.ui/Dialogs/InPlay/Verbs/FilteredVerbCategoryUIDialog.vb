Imports towd.business

Public Class FilteredVerbCategoryUIDialog
    Implements IUIDialog

    Private ReadOnly context As IUIContext(Of IWorld)
    Private ReadOnly verbCategoryType As String
    Private ReadOnly verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean)
    Private ReadOnly cancelDialog As Func(Of IUIDialog)
    Const NEVER_MIND_TEXT = "Never Mind"
    Private ReadOnly table As IReadOnlyDictionary(Of String, Func(Of IUIDialog))

    Sub New(
           context As IUIContext(Of IWorld),
           prompt As String,
           verbCategoryType As String,
           verbTypeFilter As Func(Of IVerbType, ICharacter, Boolean),
           cancelDialog As Func(Of IUIDialog))
        Me.context = context
        Me._Prompt = prompt
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
                        False,
                        Function() New FilteredVerbCategoryUIDialog(context, GetPrompt(), verbCategoryType, verbTypeFilter, cancelDialog))
               End Function
    End Function

    Private Function getTableKey(verbType As IVerbType) As String
        Return verbType.Name
    End Function

    Public Function GetLines() As IEnumerable(Of (String, String, Boolean)) Implements IUIDialog.GetLines
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
