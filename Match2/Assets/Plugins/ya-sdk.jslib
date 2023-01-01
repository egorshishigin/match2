mergeInto(LibraryManager.library, {

  SaveExtern: function (data) {
	  
	  if(player != null){
		  	var dataString = UTF8ToString(data);
	
	var json = JSON.parse(dataString);
	
	player.setData(json);
	  }

  },
  
	LoadExtern: function () {
		if(player != null){
		player.getData().then(_data => {
		const json = JSON.stringify(_data);
		console.log(json);
		myGameInstance.SendMessage('GameStatistic', 'LoadScoreData', json);
	});
		}
  },
  
  SetScrewLB: function(value){
	  ysdk.getLeaderboards()
  .then(lb => {
    lb.setLeaderboardScore('Screw', value);
  });
  },
  
    SetCatchLB: function(value){
	  ysdk.getLeaderboards()
  .then(lb => {
    lb.setLeaderboardScore('Catch', value);
  });
  },
  
  ShowFullScreenAD: function(){
	ysdk.adv.showFullscreenAdv({
    callbacks: {
        onClose: function(wasShown) {
          // some action after close
        },
        onError: function(error) {
          // some action on error
        }
    }
	})  
  },

  RewardedAD: function(){
	  ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
		  myGameInstance.SendMessage('GameOver', 'GiveNuts');
        },
        onClose: () => {
          console.log('Video ad closed.');
		  myGameInstance.SendMessage('GameOver', 'ContinuePlay');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },

});