using Domain.SeedWork;
using Domain.UserAgg;
using Resources.Messages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using static Domain.SeedWork.Constants;

namespace Domain.UserAgg;

public class Comment : Entity
{
	

	public Comment() : base()
	{

		UpdateDateTime = InsertDateTime;

	}

	
	public bool IsDeleted { get; set; }

	public bool IsEdited { get; set; }

	public bool IsVerified { get; set; }

	
	[DatabaseGenerated(DatabaseGeneratedOption.None)]

	public DateTime UpdateDateTime { get; private set; }
	
	public decimal Score { get; set; }

	[MaxLength
		(length: Constants.MaxLength.Comment,
		ErrorMessageResourceType = typeof(Validations),
		ErrorMessageResourceName = nameof(Validations.MaxLength))]
	public string Comments { get; set; }


	public void SetUpdateDateTime()
	{
		UpdateDateTime =
			Utility.Now;
	}
	
	public Guid UserId { get; set; }

	public User User { get; set; }
	

	public void SetId(Guid id)
	{
		Id = id;
	}

	//OwnerId
}
