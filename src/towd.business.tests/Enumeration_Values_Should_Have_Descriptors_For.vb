Imports Shouldly
Imports towd.data
Imports Xunit

Namespace towd.business.tests
    Public Class Enumeration_Values_Should_Have_Descriptors_For
        <Fact>
        Sub Character_Types()
            For Each sut In [Enum].GetValues(Of CharacterType)
                Try
                    sut.ToDescriptor.ShouldNotBeNull($"{sut}'s Descriptor Should Not Be Null")
                Catch ex As Exception
                    Assert.Fail($"CharacterType.{sut}'s Descriptor Should Exist")
                End Try
            Next
        End Sub
        <Fact>
        Sub Item_Types()
            For Each sut In [Enum].GetValues(Of ItemType)
                Try
                    sut.ToDescriptor.ShouldNotBeNull($"{sut}'s Descriptor Should Not Be Null")
                Catch ex As Exception
                    Assert.Fail($"{sut}'s Descriptor Should Exist")
                End Try
            Next
        End Sub
        <Fact>
        Sub Verb_Types()
            For Each sut In [Enum].GetValues(Of VerbType)
                Try
                    sut.ToVerbTypeDescriptor.ShouldNotBeNull($"{sut}'s Descriptor Should Not Be Null")
                Catch ex As Exception
                    Assert.Fail($"{sut}'s Descriptor Should Exist")
                End Try
            Next
        End Sub
    End Class
End Namespace

