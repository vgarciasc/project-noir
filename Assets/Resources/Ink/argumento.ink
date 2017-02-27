-> arjuna_cross_exam_1

VAR back_line = -> arjuna_cross_exam_1

=== arjuna_cross_exam_1 ===
= intro
-> arg1

= arg1
    - /%arjuna_normal/ I have not stolen the dog's necktie. #pressoption
    ~ back_line = -> arg2
    +   [press] -> arg1_1
    +   [dont press] -> arg2
    
= arg1_1
    - /%noir_normal/ Can you prove you didn't do it?
    ~ back_line = -> arg1_2
    +   [cont] -> wrong_contrad
    +   [dont cont] -> arg1_2
    
= arg1_2
    - /%arjuna_normal/ Is there a need to?
    ~ back_line = -> arg1_3
    +   [cont] -> wrong_contrad
    +   [dont cont] -> arg1_3

= arg1_3
    - /%arjuna_normal/ Last time I checked, there was no evidence against me, was there? #memfrag
    ~ back_line = -> arg1_4
    +   [cont] -> wrong_contrad
    +   [dont cont] 
        ++ [not] -> arg1_4
        ** [enter memfrag] -> mem_1

= arg1_4
    - /%arjuna_normal/ But rest assured - I’ll soon provide you solid reasoning that will clear up this misunderstanding. #endpress
    ~ back_line = -> arg2
    +   [cont] -> wrong_contrad
    +   [dont cont] -> arg2
    
= arg2
~ back_line = -> arg3
- /%arjuna_angry/ The spirits would never forgive me for such an immoral act, and I’d surely be met /%arjuna_smug/ with <color=green>karmic punishment</color> /%arjuna_normal/ were I to do so!
    +   [press]
        /%noir_normal/ Would the spirits really be unwilling to forgive you, the Spiritualist Prodigy? I find that unlikely.
        /%arjuna_normal/ Why, you give me more credit than I’m due, Miss Noir.
        /%arjuna_normal/ I merely communicate with the spirits - they’re my friends, to put it simply.
        /%noir_normal/ “If I had friends, I guess I could answer that….” #endpress
        -> arg3
    +   [dont press]
        -> arg3
    
= arg3
~ back_line = -> arg4
- /%arjuna_smug/ Not that I’d do it if that wasn’t the case, of course.
    +   [press]
        /%noir_normal/ “Oh? Are you really that good of a person?”
        /%arjuna_normal/ Of course I am! Do you think the spirits would lend their guidance to a man of evil thoughts?
        /%arjuna_normal/ Besides, am I not supposed to be innocent until proven otherwise? I’d expect you to give you a little more credit here!
        /%noir_normal/ “Uugh…. I guess he’s right.”
        /%arjuna_normal/ But never fear! I will soon guide you to the truth you seek for! #endpress
        -> arg4
    +   [dont press]
        -> arg4
    
= arg4
~ back_line = -> end_cycle
- After all, I wasn’t even aware that the dog wore a necktie!
    +   [press]
        /%noir_normal/ “You…. weren’t even aware?”
        /%arjuna_normal/ Yes, that would be the case.
        /%arjuna_normal/ Due to Miss Doggo’s… unique personality, I don’t usually exchange words with her.
        /%arjuna_normal/ As such, I’m not often in her dog’s presence - I never realized he wore a necktie.
        /%noir_normal/ “Even I had realized he wore a necktie…. Is Kumar really this distracted, or…?” #endpress
        -> end_cycle
    +   [dont press]
        -> end_cycle
    
= end_cycle
-  /%noir_normal/ “As it stands, I can see no fault in his testimony…. But if he is lying, his memories should betray his testimony at some point….”
-> intro

= mem_1
- Hm, is that Mr Lulu?
- How do you fare today, oh mighty keeper of the afterworldly gates?
- I see that today, as usual, you carry the white flag that guides those who are lost, don’t you?
- Woof. Woof Woof.
- Lulu, what are you doing? Leave this man’s presence at once!
- Woof.
- It’s fine, noble keeper. May your path be filled with the graces of Buddha.
-> arg1

= wrong_contrad
- /%arjuna_smug/ WRONG!
-> back_line

=== arjuna_cross_exam_3 ===
- DAMN SON, WHERE'D YOU FIND THIS?
-> arjuna_cross_exam_1