Public Interface IVerbType
    ReadOnly Property VerbType As String
    ReadOnly Property VerbCategoryType As String
    Function CanPerform(character As ICharacter) As Boolean
    Sub Perform(character As ICharacter)
    ReadOnly Property Name As String
    ReadOnly Property Description As String
End Interface
