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
                Function(x) getTableValue(_Prompt, x))
    End Sub

    Private Function getTableValue(prompt As String, verbType As IVerbType) As Func(Of IUIDialog)
        Return Function()
                   Return New VerbDetailUIDialog(
                        context,
                        verbType,
                        False,
                        MakeCopy)
               End Function
    End Function

    Private Function getTableKey(verbType As IVerbType) As String
        Return verbType.Name
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
        Return (Function() New FilteredVerbCategoryUIDialog(context, _Prompt, verbCategoryType, verbTypeFilter, cancelDialog))
    End Function
End Class
