using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSheetEnemy : ScoreSheet
{
    
    public override Score GetScore(float remainingHealthPercent, float damageDealtPercent, string collidedTag) {
        // remainingHealthPercent is given *before* adding the damage dealt

        if (collidedTag == "Player") {
            return GetScorePlayer(remainingHealthPercent, damageDealtPercent);
        } else if (collidedTag == "Enemt") {
            return GetScoreEnemy(remainingHealthPercent, damageDealtPercent);
        } else {
            return GetScoreEnvironment(remainingHealthPercent, damageDealtPercent);
        }

    }

    private Score GetScorePlayer(float remainingHealthPercent, float damageDealtPercent) {
        // one-hit
        if (remainingHealthPercent > 0.95 && damageDealtPercent >= 1) {
            return new Score("ONE-HIT K.O", 1500);
        }

        // overkill
        if (remainingHealthPercent < 0.20 && damageDealtPercent > 2 * remainingHealthPercent) {
            if (damageDealtPercent < 0.50) {
                return new Score("Excessive damage!", 80);
            }
            return new Score("Overkill!", 100);
        }

        // finisher
        if (remainingHealthPercent <= damageDealtPercent) {
            if (damageDealtPercent < 0.10) {
                return new Score("Enemy down!", 25);
            }
            if (damageDealtPercent < 0.30) {
                return new Score("Finishing blow!", 75);
            }
            if (damageDealtPercent < 0.50) {
                return new Score("Demolished!", 150);
            }
            return new Score("BRUTALIZED!", 150);
        }

        // too low
        if (damageDealtPercent < 0.05) {
            return null;
        }

        // regular hit
        if (damageDealtPercent < 0.10) {
            return new Score("Barely grazed", 10);
        }
        if (damageDealtPercent < 0.30) {
            return new Score("Contact!", 50);
        }
        if (damageDealtPercent < 0.50) {
            return new Score("Clean hit!", 100);
        }
        if (damageDealtPercent < 0.80) {
            return new Score("Powerful blow!", 200);
        }
        return new Score("CRUSHONG BLOW!", 500);

    }
    
    private Score GetScoreEnemy(float remainingHealthPercent, float damageDealtPercent) {
        // finisher
        if (remainingHealthPercent <= damageDealtPercent && damageDealtPercent > 0.10) {
            if (damageDealtPercent < 0.30) {
                return new Score("Finishing blow! (by their own friend!)", 100);
            }
            if (damageDealtPercent < 0.50) {
                return new Score("Demolished! (by their own friend!)", 200);
            }
            return new Score("BRUTALIZED! (by their own friend!)", 400);
        }

        // too low
        if (damageDealtPercent < 0.05) {
            return null;
        }

        // regular hit
        if (damageDealtPercent < 0.10) {
            return new Score("Friendly scratch!", 10);
        }
        if (damageDealtPercent < 0.30) {
            return new Score("Friendly collision!", 50);
        }
        if (damageDealtPercent < 0.50) {
            return new Score("Friendly fire!", 100);
        }
        if (damageDealtPercent < 0.80) {
            return new Score("Crushing friendly fire!", 200);
        }
        return new Score("MUTUAL DESTRUCTION!", 500);

    }

    private Score GetScoreEnvironment(float remainingHealthPercent, float damageDealtPercent) {
        // finisher
        if (remainingHealthPercent <= damageDealtPercent && damageDealtPercent > 0.10) {
            if (damageDealtPercent < 0.30) {
                return new Score("Finished! (by a wall!)", 100);
            }
            if (damageDealtPercent < 0.50) {
                return new Score("Demolished! (on a wall!)", 200);
            }
            if (damageDealtPercent < 0.80) {
                return new Score("SPLATTERED! (on a wall!)", 400);
            }
        }

        // too low
        if (damageDealtPercent < 0.05) {
            return null;
        }

        // regular hit
        if (damageDealtPercent < 0.10) {
            return new Score("Wall scratch!", 10);
        }
        if (damageDealtPercent < 0.30) {
            return new Score("Wall hit!", 50);
        }
        if (damageDealtPercent < 0.50) {
            return new Score("Wall bash!", 100);
        }
        if (damageDealtPercent < 0.80) {
            return new Score("Wall crush!", 200);
        }
        return new Score("WALL CRUSH!", 500);

    }


}
