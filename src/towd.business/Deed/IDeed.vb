Public Interface IDeed
    ReadOnly Property Deed As data.Deed
    ReadOnly Property Name As String
    ReadOnly Property Description As String
    Sub [Do](character As ICharacter)
    Function IsAvailable(character As ICharacter) As Boolean
    Function HasDone(character As ICharacter) As Boolean
End Interface
