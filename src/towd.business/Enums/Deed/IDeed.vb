Public Interface IDeed
    ReadOnly Property Deed As String
    ReadOnly Property Name As String
    ReadOnly Property Description As String
    ReadOnly Property XP As Integer
    Sub [Do](character As ICharacter)
    Function IsAvailable(character As ICharacter) As Boolean
    Function HasDone(character As ICharacter) As Boolean
End Interface
