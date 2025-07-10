Imports Shouldly
Imports towd.business
Imports towd.data
Imports Xunit

Public Class Topics_Should_Exist_For
    <Fact>
    Public Sub Verb_Types()
        For Each sut In [Enum].GetValues(Of VerbType)
            Try
                Topics.VerbTypeTopicTable.ContainsKey(sut).ShouldBeTrue($"VerbType.{sut} Should Have a Topic")
            Catch ex As Exception
                Assert.Fail($"VerbType.{sut} Should Have a Topic")
            End Try
        Next
    End Sub
End Class
