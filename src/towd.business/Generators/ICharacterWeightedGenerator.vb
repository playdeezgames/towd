Public Interface ICharacterWeightedGenerator
    Function GetMinimum(character As ICharacter) As Integer
    Function GetMaximum(character As ICharacter) As Integer
    Function Generate(character As ICharacter) As Integer
End Interface
