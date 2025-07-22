Friend Class FixedCharacterWeightedGenerator
    Implements ICharacterWeightedGenerator
    Private ReadOnly fixedAmount As Integer
    Sub New(fixedAmount As Integer)
        Me.fixedAmount = fixedAmount
    End Sub

    Public Function Generate(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.Generate
        Return fixedAmount
    End Function

    Public Function GetMinimum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMinimum
        Return fixedAmount
    End Function

    Public Function GetMaximum(character As ICharacter) As Integer Implements ICharacterWeightedGenerator.GetMaximum
        Return fixedAmount
    End Function

    Public Overrides Function ToString() As String
        Return $"{fixedAmount}"
    End Function
End Class
