Imports Shouldly
Imports towd.business
Imports Xunit

Public Class Topics_Should_Exist_For
    <Fact>
    Public Sub Recipe_Types()
        For Each sut In [Enum].GetValues(Of VerbType)
            Try
                Topics.RecipeTypeTopicTable.ContainsKey(sut).ShouldBeTrue($"{sut} Should Have a Topic")
            Catch ex As Exception
                Assert.Fail($"{sut} Should Have a Topic")
            End Try
        Next
    End Sub
End Class
