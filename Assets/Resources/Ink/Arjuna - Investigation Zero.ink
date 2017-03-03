->CrossExamination_I

===Intro===

-The green person slowly climbed the long, intricate stairs that led to the top of the temple, his steps resolute. It didn’t took him long until he reached his destination.

-As he set his foot on the top of the temple, he stared at his foe, sitting in his golden throne.

-At first glance, one could mistake the man sitting there for an ordinary one. While handsome, his features were very much like those of his people - black hair, tanned skin and strong complexion.

-What set him apart from his peers, however, were his eyes - these deep, enchanting purple eyes who were clearly not those of a human. 

-That person was, without a doubt, someone with divine blood.

-The green person, without hesitation, declared his intentions to the purple-eyed man:

- /%doggo_unknown/ JUST SHUT UP AND CONFESS YOUR CRIMES ALREADY, YOU SHADY MONK! #Speaker_???

-…. I take a deep breath and ignore the exacerbated shout.

- /%green_person_normal/ Oh, King among Men. You, who in the height of your arrogance believes yourself to be above the Gods - by my hands, I shall deliver the punishment for your foolishness.” #Speaker_Green_Person

-The king stared at the green person for a few seconds, tension filling the air. He then burst into laughter.

-After composing himself, the king replied:

- /%king_normal/ How amusing, to think the Gods would send an envoy with such great sense of humor! And how fortunate - I’ve just claimed the head of our previous Jester, so this court is in dire need of one. #Speaker_King

- /%king_normal/ While dropping the stoic face would surely benefit your performance, I, the King, will be willing to accept you even in your inexperience. #Speaker_King

-The green person remained quiet in face of the king’s mockery. Irritated at the lack of a response, his opponent raised his tone:

- /%doggo_unknown/ I’M TELLING YOU IT’S USELESS! THE FILTHY SMELL OF YOUR LIES WILL NOT ESCAPE MY LULU!!! #Speaker_???

-I let out a deep sigh, pick up my bookmark, place it on the beginning of the green person and the king’s confrontation and close the book slowly.

-I take a look around the classroom to see what kind of ridiculous situation is going on this time. It doesn’t take me long to find out it’s source.

-/%arjuna_unknown/ My, my, Miss Doggo. Please enlighten me, how do lies smell? Is it a sweet perfume, like that of a thorny rose, or is it a foul stench, like that of your precious companion’s feces? #Speaker_???

-/%doggo_unknown/ H-how should I know, you fool? I’m a dog tamer, not a dog myself! I would appreciate if you’d drop the foolish questions!” #Speaker_???

- /%arjuna_unknown/ Haha, I suppose I must apologize for that, Miss Doggo, but you left yourself way too open - consider it karmic retribution, shall we? #Speaker_???

-Sitting in his table without a worry in the world is Arjuna Kumar, Spiritualist Prodigy. And fiercely interrogating him - or rather, demanding his confession - is Miss Doggo, Taming Prodigy.

-I let out another deep sigh. Nothing good could come out of involving myself with these two’s affairs, but I’m afraid my reading can only continue after I deal with whatever their trouble is.

- /%noir_normal/ Excuse me, what is going on here?” #Speaker_Noir

- /%doggo_angry/ Hmph, none of YOUR concern, Noir. I don’t need any detective wannabes to tell me this shady monk is the culprit here.” #Speaker_Doggo

- /%arjuna_smug/ Why so hasty, Miss Doggo? I, for one, see Miss Noir’s involvement with this case as a very fortunate omen - the spirits are in much better mood now that she has arrived!” #Speaker_Arjuna

- /%doggo_angry/ As if I care about your so-called spirits! All I need is my Lulu’s trusty nose to discern the truth! And it’s nose tells me you’re the culprit! #Speaker_Doggo

- /%doggo_angry/ Now, surrender peacefully, or I’ll have Lulu’s mighty fangs work their way through you! #Speaker_Doggo

- /%arjuna_smug/ You know, this would be a far more effective threat if you gave your dog a more intimidating name - being ran by Lulu’s fangs doesn’t sound that worrisome, I’m afraid. #Speaker_Arjuna

-...This is going nowhere fast. Sigh, why did it have to be these two? Why aren’t there any more developed characters to work with on this demo?

-  /%noir_normal/ I’d appreciate if you’d keep your dog away from the suspect, Miss Doggo. Now, Mister Kumar, could you please elaborate about what exactly is going on here? #Speaker_Noir

-…  ...  .  .  .

- /%noir_normal/ So, let me get this straight. Someone stole Miss Doggo’s dog’s necktie, and she says it greatly resembles the blindfold you’re currently wearing. Is that correct? #Speaker_Noir

- /%arjuna_normal/ I suppose that’s the case, yes. #Speaker_Arjuna

-...This is beyond silly. I guess I’d better take notes of all this, in any case. Giving my notebook a check during investigation is also a good idea.

- /%doggo_angry/ That’s not all! My Lulu has tracked his necktie all the way to here! Therefore, this must undeniably be his necktie, and this shady monk must be the culprit! #Speaker_Doggo

- /%noir_normal/ I’d appreciate if you kept your conclusions to yourself, Miss Doggo. By narrowing down our possibilities, we stray further from the truth.” #Speaker_Noir

- /%doggo_angry/ Hmpf! Do as you want, but you’re just wasting your time. And by that, I mean you're wasting MY time too! #Speaker_Doggo

- /%noir_normal/ *sigh* Very well. Mr. Kumar, would you mind if I asked you a few questions? #Speaker_Noir

- /%arjuna_normal/ Not at all. The Lord is well aware of my innocence, and through His guidance I’m sure that I will leave this incident with a Not Guilty verdict. #Speaker_Arjuna

-...I wasn’t aware we were standing on a courtroom. And since when is he a priest….?

->CrossExamination_I

VAR ReturnTo = -> CrossExamination_I

==CrossExamination_I==

->Statement1

=Statement1
    -/%arjuna_normal/ I have not stolen the dog’s necktie. #pressoption #Speaker_Arjuna
        +[Press the Argument] ->Statement1Press1
        +[Leave it be.] ->Statement2

=Statement1Press1
    -/%noir_normal/ Can you prove you didn’t do it? #Speaker_Noir
    -/%arjuna_normal/ Is there a need to? #Speaker_Arjuna #obj #clue_monster1
        ~ReturnTo = ->Statement1Press2
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement1Press2
        +[Object with "White Flag"] -> CEI_Objection 

=Statement1Press2
-/%arjuna_normal/ Last time I checked, there was no evidence against me, was there? #Speaker_Arjuna #obj
        ~ReturnTo = ->Statement1Press3
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement1Press3
    
=Statement1Press3
-/%arjuna_normal/ But rest assured - I’ll soon provide you solid reasoning that will clear up this misunderstanding. #Speaker_Arjuna #obj #endpress
        ~ReturnTo = -> Statement2
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement2
    
=Statement2
-/%arjuna_normal/ The spirits would never forgive me for such an immoral act, and I’d surely be met with karmic punishment were I to do so! #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement2Press1
        +[Leave it be.] ->Statement3

=Statement2Press1
    -/%noir_normal/ Would the spirits really be unwilling to forgive you, the Spiritualist Prodigy? I find that unlikely. #Speaker_Noir
    -/%arjuna_normal/ Why, you give me more credit than I’m due, Miss Noir. #Speaker_Arjuna
        ~ReturnTo = -> Statatement2Press2
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statatement2Press2

=Statatement2Press2
    -/%arjuna_normal/ I merely communicate with the spirits - they’re my friends, to put it simply. #Speaker_Arjuna #obj
        ~ReturnTo = -> Statement2Press3
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement2Press3

=Statement2Press3
    -/%arjuna_smug/ Wouldn’t you be mad at your friend if he were to do such a thing? #Speaker_Arjuna #obj
        ~ReturnTo = -> Statement2Ending
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement2Ending
        
=Statement2Ending
    -/%noir_sad/ If I had friends, I guess I could answer that… #Speaker_Noir #Thinking #endpress
->Statement3

=Statement3
    -/%arjuna_normal/ Not that I’d do it if that wasn’t the case, of course. #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement3Press1
        +[Leave it be.] ->Statement4
    
=Statement3Press1
    -/%noir_normal/ Oh? Are you really that good of a person? #Speaker_Noir
    -/%arjuna_smug/ Of course I am! Do you think the spirits would lend their guidance to a man of evil thoughts? #Speaker_Arjuna #obj
        ~ReturnTo = ->Statement3Press2
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement3Press2

=Statement3Press2
    -/%arjuna_smug/ Besides, am I not supposed to be innocent until proven otherwise? I’d expect you to give you a little more credit here. #obj
        ~ReturnTo = ->Statement3Press3
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement3Press3

=Statement3Press3        
    -/%noir_sad/ Uugh…. I guess he’s right. #Speaker_Noir #Thinking 
    -/%arjuna_smug/ But never fear! I will soon guide you to the truth you seek for! #Speaker_Arjuna #obj #endpress
        ~ReturnTo = ->Statement4
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement4
        
=Statement4
    -/%arjuna_normal/ After all, I wasn’t even aware that the dog wore a necktie! #Speaker_Arjuna #pressoption #memfrag
        +[Press the Argument] ->Statement4Press1
        +[Leave it be.] ->CEI_NeutralEnd
        *[Dive into his memories.] ->MF_Talking_to_Dog
    
=MF_Talking_to_Dog
    -/%arjuna_normal/ Hm, is that Mr Lulu? #Speaker_Arjuna
    -/%arjuna_normal/ How do you fare today, oh mighty keeper of the afterworldly gates? #Speaker_Arjuna
    -/%arjuna_normal/ I see that today, as usual, you carry the white flag that guides those who are lost, don’t you? #Speaker_Arjuna
    -/%lulu_normal/ Woof. Woof Woof. #Speaker_Lulu
    -/%doggo_angry/ Lulu, what are you doing? Leave this man’s presence at once! #Speaker_Doggo
    -/%lulu_normal/ Woof. #Speaker_Lulu
    -/%arjuna_normal/ It’s fine, noble keeper. May your path be filled with the graces of Buddha. #Speaker_Arjuna
    - Obtained “White Flag”
->Statement4

=Statement4Press1
    -/%noir_normal/ You…. weren’t even aware? #Speaker_Noir
    -/%arjuna_normal/ Yes, that would be the case. #Speaker_Arjuna #obj
        ~ReturnTo = ->Statement4Press2
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement4Press2

=Statement4Press2
    -/%arjuna_normal/ Due to Miss Doggo’s… unique personality, I don’t usually exchange words with her. #Speaker_Arjuna #obj
        ~ReturnTo = ->Statement4Press3
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement4Press3
        
=Statement4Press3
    -/%arjuna_normal/ As such, I’m not often in her dog’s presence - I never realized he wore a necktie. #Speaker_Arjuna #obj #clue_MF_Talking_to_Dog 
         ~ReturnTo = ->Statement4
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement4Ending
        +{MF_Talking_to_Dog} [Object with "White Flag"] -> CEI_Objection 
        
=Statement4Ending
    -/%noir_normal/ Even I had realized he wore a necktie…. Is Kumar really this distracted, or…? #Speaker_Noir #Thinking #endpress
->CEI_NeutralEnd


=CEI_NeutralEnd
-/%noir_normal/ As it stands, I can see no fault in his testimony…. But if he is lying, his memories should betray his testimony at some point… #Speaker_Noir #Thinking
->Statement1

=CEI_Objection
    - !!!!!!!!!!!!!!!!!!!!
    -/%noir_normal/ Mr Kumar, I don’t suppose you intend me to believe that, do you? #Speaker_Noir
    -/%arjuna_smug/ Excuse me? I spoke nothing but the truth, you see. #Speaker_Arjuna
    -/%noir_normal/ In that case, I’ll have to object to that. #Speaker_Noir
    -/%noir_normal/ Mr Kumar, aren’t you familiar enough with the Dog’s Necktie to call it… ahem, a ‘White Flag that guides those who are lost?’ #Speaker_Noir
    -/%arjuna_shocked/ !!!!!!!!!!!!!!!!!!! #Speaker_Arjuna
    -/%arjuna_shocked/ H-how do you know that?!?! #Speaker_Arjuna
    -/%noir_normal/ How I know it is irrelevant, Mr Kumar. What is relevant here is that your testimony just now was a lie. #Speaker_Noir
    -/%arjuna_shocked/ W-wait a second, Miss Noir! Calling it a lie is exaggerating! /%arjuna_normal/ Please, allow me to make up for my mistake! #Speaker_Arjuna
->CrossExamination_II

==CrossExamination_II==
->Statement1

=Statement1
    -/%arjuna_normal/ I’ll admit to having knowledge of the Necktie, all right. May the spirits forgive me for that terrible lie. #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement1Press1
        +[Leave it be.] ->Statement2
    
=Statement1Press1
    -/%noir_normal/ Is that all the spirits will have to forgive you from? #Speaker_Noir
    -/%arjuna_smug/ Now now, Miss Noir. Such a heavy judgement is unbefitting from you. #Speaker_Arjuna #obj
        ~ReturnTo = -> Statement1Press2
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement1Press2

=Statement1Press2
    -/%arjuna_normal/ ’Hate the sin, love the sinner.’ Isn’t that how it goes? #Speaker_Arjuna #obj
        ~ReturnTo = ->Statement1Ending
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement1Ending

=Statement1Ending
    -/%noir_normal/ I’d really appreciate if he chose a religion and stuck to it… #Speaker_Noir #Thinking #endpress
->Statement2

=Statement2
    -/%arjuna_normal/ However, I must still deny stealing it. #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement2Press1
        +[Leave it be.] ->Statement3
    
=Statement2Press1
    -/%noir_normal/ You… must? #Speaker_Noir
    -/%arjuna_smug/ Now now, Miss Noir, is it really appropriate for a detective to object to the suspect’s choice of words? #Speaker_Arjuna #obj
        ~ReturnTo = -> Statement2Press2
        +[Object with Whatever] ->Wrong_Objection
        +[Leave it be.] ->Statement2Press2

=Statement2Press2
    -/%arjuna_smug/ I am innocent. Therefore, I must deny being guilty. Do you have any objections to that? #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement2Press3
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement2Press3
            
=Statement2Press3
    -/%arjuna_normal/ Then may we proceed, with the blessings of the Buddha. #Speaker_Arjuna #obj #endpress
            ~ReturnTo = -> Statement3
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement3    
            
=Statement3
    -/%arjuna_normal/ Miss Doggo’s claim is that it rests on my face at this very moment, isn’t it? #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement3Press1
        +[Leave it be.] ->Statement4

=Statement3Press1    
    -/%noir_normal/ That’s what I’ve heard, yes. Do you deny it? #Speaker_Noir
    -/%arjuna_normal/ Patience, Miss Noir. All shall be revealed as the heavens deem fitting. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement3Press2
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement3Press2    

=Statement3Press2
    -/%arjuna_normal/ Hm...? Oh, is that so? I see.... /%arjuna_smug/ How fortunate! It so happens that they deem it fitting to be revealed in my next statement. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement3Press3
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement3Press3

=Statement3Press3
    -/%arjuna_smug/ I’d ask you to wait patiently as I once again guide you closer to the truth. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement3Ending
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement3Ending

=Statement3Ending
    -/%noir_sad/ I’d say you’ve been guiding me AWAY from it…. #Speaker_Noir #Thinking #endpress
->Statement4

=Statement4
    -/%arjuna_normal/ That’s all but impossible. As the spirits would know, I’ve been wearing this blindfold ever since I left my dorm room! #pressoption
        +[Press the Argument] ->Statement4Press1
        +[Leave it be.] ->CEII_NeutralEnd

=Statement4Press1
    -/%noir_normal/ That… would be the entire day. You’ve been wearing it this whole time? #Speaker_Noir
    -/%arjuna_normal/ I’d have believed it to be clear enough, Miss Noir. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement4Press2
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement4Press2    

=Statement4Press2
    -/%arjuna_normal/ Ever since I left my dorm room, I didn’t took my blindfold off once. #Speaker_Arjuna #obj #clue_Medical_Checkup
            ~ReturnTo = -> Statement4Ending
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement4Ending
            +[Object with Medical Checkup.] -> CEII_Objection

=Statement4Ending
    -/%noir_normal/ He sure is fond of that blindfold… #Speaker_Noir #endpress
->CEII_NeutralEnd

=CEII_NeutralEnd
    -/%noir_normal/ Wearing a blindfold the entire day…. Is that even possible? Well, I kept my hair over my eye the entire day…. Wait, did I? I’m not so sure… #Speaker_Noir #Thinking
->Statement1

=CEII_Objection
    -/%noir_normal/ Mr. Kumar, can you remind me of which day of the week it is today? #Speaker_Noir
    -/%arjuna_normal/ Hm… Although some religions would have different calendars, I’m led to believe today is a Wednesd /%arjuna_shocked/ -- ack! #Speaker_Arjuna
    -/%noir_normal/ You’ve realized it already, didn’t you? We all have medical checkups on Wednesday. I find it very odd that the doctor didn’t check your eyes during it, Mr. Kumar. #Speaker_Noir
    -/%arjuna_shocked/ B-but I’m blind! A lot of doctors wouldn’t do it! #Speaker_Arjuna
    -/%noir_smug/ Are you implying that the doctor hired by the Human Order Preservation Environment would incur in such a silly malpractice?
    -/%arjuna_sad/ ....Okay, I concede. I did remove my blindfold during the checkup. #Speaker_Arjuna
    -/%noir_normal/ All right, I’m piercing through his lies one by one. The end shouldn’t be too far. #Speaker_Noir #Thinking
    -/%noir_normal/ Mr Kumar, may I please ask you to testify about your actions during the checkup? Also, it would be helpful if you were completely honest in your testimony this time. #Speaker_Noir
    -/%arjuna_normal/ .... #Speaker_Arjuna
->CrossExamination_III

===CrossExamination_III===

->Statement1

=Statement1
    -/%arjuna_normal/ Yes, the doctor did ask me to remove my blindfold. #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement1Press1
        +[Leave it be.] ->Statement2

=Statement1Press1
    -/%noir_normal/ And I’m assuming you did it? #Speaker_Noir
    -/%arjuna_normal/ Yes, I did. I gave the doctor my blindfold, and I retrieved it from her after the checkup was done. #Speaker_Arjuna #obj #clue_Medical_Checkup
            ~ReturnTo = -> Statement1Press2
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement1Press2
            +[Object with Medical Checkup.] -> CEIII_Objection

=Statement1Press2
    -/%arjuna_normal/ May the Gods smite me if I’m lying right now. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement1Ending
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement1Ending

=Statement1Ending
    -/%noir_normal/ So he is a polytheist now…? #Speaker_Noir #Thinking #endpress
->Statement2

=Statement2
    -/%arjuna_normal/ But I’m wearing the same blindfold right now. #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement2Press1
        +[Leave it be.] ->Statement3

=Statement2Press1
    -/%noir_normal/ And? Can you prove it? #Speaker_Noir
    -/%arjuna_normal/ Don’t ask me. Ask the doctor. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement2Press2
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement2Press2

=Statement2Press2
    -/%arjuna_normal/ And before you ask, no, I didn’t touch my blindfold after the checkup was done. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement2Press3
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement2Press3

=Statement2Press3
    -/%arjuna_normal/ The blindfold I’ve wore after the checkup is the same I’m wearing right now. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement2Ending
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement2Ending

=Statement2Ending
    -/%noir_normal/ He’s oddly resolute right now… Yet something about this feels off.... #Speaker_Noir #Thinking #endpress
->Statement3

=Statement3
    -/%arjuna_normal/ As such, I can’t be the thief! #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement3Press1
        +[Leave it be.] ->CEIII_NeutralEnd

=Statement3Press1
    -/%noir_normal/ ...W-What if you’re just keeping the blindfold on your pocket, and that’s where the smell is coming from?!? #Speaker_Noir
    -/%arjuna_normal/ Surely you jest, Miss Noir. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement3Press2
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement3Press2

=Statement3Press2
    -/%arjuna_smug/ After all, that’s such a childish remark that I’d doubt your proficiency as a Detective were it not a jest. #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement3Press3
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement3Press3

=Statement3Press3
    -/%arjuna_smug/ However, the Lord is merciful, and, as his envoy, I shall extend unto thee His mercy and forgive you even said it! I’m such a good person, am I not? I wonder why I’m even being suspected of theft!  #Speaker_Arjuna #obj
            ~ReturnTo = -> Statement3Ending
            +[Object with Whatever] ->Wrong_Objection
            +[Leave it be.] ->Statement3Ending

=Statement3Ending
    -/%noir_sad/ Was that such a weak remark that even Arjuna is mocking me…? /%noir_normal/ Better think twice before my next objection. #Speaker_Noir #Thinking #endpress
->CEIII_NeutralEnd

=CEIII_NeutralEnd
    -/%noir_normal/ I can see no flaw in his testimony so far… But he was trying to hide the Checkup…. He must be hiding something else about it. #Speaker_Noir #Thinking
->Statement1

=CEIII_Objection
    -/%noir_normal/ Mr. Kumar, there is a blatant contradiction in this statement! #Speaker_Noir 
    -/%arjuna_normal/ Now, Miss Noir, I’m afraid you’re just grasping at the straws. The Heavens can attest that I did not lie in my previous statement. #Speaker_Arjuna
    -/%arjuna_normal/ You say the Medical Checkup Report contradicts me? Where is that contradiction? #Speaker_Arjuna
->SelectObjection

=SelectObjection
        +[It's on the first page.] ->Objection_Sucessful
        +[It's on the second page.] ->Objection_Failed
        +[It's on the third page.] ->Objection_Failed

=Objection_Failed
    -/%noir_smug/ Here is your contradiction for you! #Speaker_Noir
    -/%arjuna_smug/ Hm…. the spirits fail to pick any negative energies from that page you’ve shown me. Are you sure the Report contradicts me? #Speaker_Arjuna
    -/%noir_sad/ Ugh…. Better rethink that one.
->SelectObjection

=Objection_Sucessful
    -/%noir_normal/ Mr. Kumar, take a look at the name of the Doctor written in this report. #Speaker_Noir
    -/%arjuna_angry/ Miss Noir, now that’s just in poor taste. Asking a blind person to read a report? I’d have expected more from you. #Speaker_Arjuna
    -/%noir_shocked/ Ack! /%noir_sad/ Ahem, Hm, erm, I - I apologize. /%noir_normal/ In any case, the name written in this report is Guy Fooks. Does that sound odd to you? #Speaker_Noir
    -/%arjuna_shocked/ Wait, Dr. Fooks?! T-That’s impossible! I’m sure my checkup was done by a woman! #Speaker_Arjuna
    -/%noir_normal/ That’s not what the report says, mind you.
    -/%arjuna_shocked/ P-perhaps the boys  are checked by a female doctor, while the girls are checked by a male one?
    -/noir_normal/ ...Let's accept that reasoning for now. /%noir_smug/ Do tell, Mr. Kumar - were any of your previous checkups done by a female doctor? #Speaker_Noir
    -/%arjuna_sad/ ...No. I’ve always been checked up by Dr. Fooks. #Speaker_Arjuna
    -/%noir_normal/ Will you confess to your lie, then? #Speaker_Noir
    -/%arjuna_shocked/ N-n-nonono! Please, wait a second! I swear in the Lord’s name, I did not lie about this! I was checked up by a female doctor! #Speaker_Arjuna
    -/%noir_normal/  Hmm, now this is odd…. Though he contradicts the evidence, his mind isn’t betraying his words…. Is he really saying the truth? #Speaker_Noir #Thinking
    -/%noir_normal/ Mr. Kumar. I’ll have you testimony about your medical checkup in full length. #Speaker_Noir
    -/%arjuna_normal/ Yes, Miss Noir! I’ll have you see it for yourself that I’m not lying about this! #Speaker_Arjuna
->CrossExamination_IV

===CrossExamination_IV===
->Statement1

=Statement1
    -/%arjuna_normal/ I found it weird that Dr. Fooks wasn’t the the doctor doing the check-up this time. #Speaker_Arjuna #pressoption
        +[Press the Argument] ->Statement1Press1
        +[Leave it be.] ->Statement1

=Statement1Press1
    -/%noir_normal/ And you did nothing about it? #Speaker_Noir
    >The spirits recognized her as a legitimate doctor, so I believed in their judgement.
    >Besides, all will be made clear as I continue my testimony.
    >(Noir)(Thinking): “I wish he wouldn’t rely so much on his spirits, to be honest….”
    
    >But it all went normally! No weird stuff going on!
    [Memory Fragment: The Medical Check-Up]
    So, you said your name was….?(Arjuna)<
    I’m Doctor Tiala. Pleased to make your acquaintance.(???)<
    She has a surprisingly childish voice….(Thinking)(Arjuna)< 
    So, Mr. Kumar, I’ll ask you to drink this. (Tiala)<
    Hm….? What is that for? (Arjuna)<
    A light anesthetic. Just in case.(Tiala)<
    There is no weird scent coming from this….(Thinking).(Arjuna)<
    The spirits find no bad omen in this, so I shall oblige.(Arjuna)<
    What’s this? I’m feeling… a little…. odd….(Thinking)(Arjuna)<
    ...<
    We’re all done, Mr. Kumar. Here, have your blindfold.(Tiala)<
    I appreciate it, Doctor Tiala.(Arjuna)<
    It feels a little weird…. But the smell is still the same...(Thinking)(Arjuna)<
    Obtained <<Anesthetic>> <
    >(Noir): That just sounded a little odd. Can you elaborate?
    >O-of course I can!
    >The doctor just ran me through the ordinary procedures. Just as any check-up!
    [Object to this statement with <<Anesthetic>>]4
    >She then returned me my blindfold.
    >(Noir)(Thinking): “The ordinary procedures? He seems a little unsure of it… If only I could see how this check-up went….”
    
    >I then left the Infirmary with my own blindfold! And that’s the whole truth!
    >(Noir): Are you completely sure she returned you your own blindfold?
    >C-completely sure, that I am!
    >After all, the spirits kept track of her every movement for me!
    >So she didn’t had the time to do anything suspicious at all!
    [Object to this statement with <<Anesthetic>>]4
    >(Noir)(Thinking): “She never had the time….? That’s an odd choice of words…. If only I could confirm that claim….”
    
    >(Noir)(Thinking): “There is clearly a strange part in his testimony…. I better press that for more information.”
    
    
    4>”I CAN SEE THE TRUTH NOW!” 
    
    >(Noir): “Mr. Kumar…. During the check-up, the doctor tending to you made you ingest a certain substance, correct? I believe they were…. Anesthetics?”
    >(Arjuna): “*GULP*”
    >(Noir): “I’ll take your silence as confirmation. Mr. Kumar, you do realize that, during the time you were asleep due to the effect of the anesthetics…. The doctor could have easily swapped your blindfold, correct?”
    >(Arjuna): “T-that’s preposterous! T-the spirits would never allow for such a thing!”
    >(Noir): “Do your spirits warn you of things that happen during your sleep, Mr. Kumar?”
    >(Arjuna): “Ugh….”
    >(Noir): “Furthermore… would you be able to say, with full confidence, that this blindfold is indeed your own?”
    >(Arjuna):”UUUUUUUUGH…..”
    >(Noir): “With all things considered, it seems natural to me that the blindfold you’re currently wearing…. IS MS. DOGGO’S DOG’S NECKTIE!”
    >(Arjuna): “DIOS MIOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO!!!!!!!!!!!!!’
    
    >(Noir)(Thinking): “I’ve seen it all now…. That’s the truth he was hiding.”
    
    ->DONE

==Wrong_Objection==
    -/%noir_smug/ Take a look at that, Mr. Kumar.
    -/%arjuna_normal/ Uh... What about it, Miss Noir? Do you want me to check if there are any evil spirits possessing it? This won't be free of charge, of course.
    -/%noir_angry/ N-no! This is the proof that you're lying!
    -/%arjuna_normal/ Uh.... This has nothing to do with my previous statement, Miss Noir.
    -/%noir_shocked/ It doesn't?!?! /%noir_sad/ B-But I thought...
    -/%arjuna_smug/ Now, now, there is no need for excuses. I understand it perfectly well. 
    -/%noir_normal/ R-really?
    -/%arjuna_smug/ Yes!My powerful aura is clouding your judgement, leading you to make mistakes in your assertions!
    -/%arjuna_smug/ I sometimes forget about how strong of an effect my powers have on those around me. I suppose I must redicrect my energy towards....
    -/%noir_sad/ *sigh* Someone, please make him stop...
->ReturnTo