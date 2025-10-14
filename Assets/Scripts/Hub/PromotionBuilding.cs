using UnityEngine;

public class PromotionBuilding : MonoBehaviour
{
    private FollowerStatblock followerStats { get; set; }
    private IClasses currClass { get; set; }


    public void PromoteFollower()
    {
        if (isFollowerStatsReady() && isItemsReady())
        {
            currClass.DoPromotion();
        }
        else
        {

        }
    }


    private bool isFollowerStatsReady()
    {
        bool ret = false;
        if(followerStats != null && currClass != null)
        {
           if(followerStats.vigor >= currClass.promotionVigor && followerStats.power >= currClass.promotionPower && followerStats.resilience >= currClass.promotionResilience
                && followerStats.magick >= currClass.promotionMagick && followerStats.faith >= currClass.promotionFaith && followerStats.agility >= currClass.promotionAgility)
            {
                ret = true;
            }
        }
        else
        {
            Debug.LogError("The follower was null or their stats were");
        }
            return ret;
    }

    //LB: Items will either be generic all promotions cost 1 promotion rock, or the promotion will cost specific items. If it's generic this can be done here, but if it's not 
    //LB: it will need to be done in the specific class scripts and checked here.
    private bool isItemsReady()
    {
        bool ret = false;
        // Check the player's items here, will be implemented later
        ret = true;
        return ret;
    }

}
