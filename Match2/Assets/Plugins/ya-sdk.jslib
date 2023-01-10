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
		myGameInstance.SendMessage('Game', 'LoadData', json);
	});
		}
  },
  
  SetLB: function(value){
	  ysdk.getLeaderboards()
  .then(lb => {
    lb.setLeaderboardScore('Level', value);
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

  ShopRewarded: function(){
	  ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
		  myGameInstance.SendMessage('Game', 'GiveShopStars');
        },
        onClose: () => {
          console.log('Video ad closed.');
		  myGameInstance.SendMessage('AD_fx', 'PlayParticle');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },
  
    ExtraStarsRewarded: function(){
	  ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
		  myGameInstance.SendMessage('Game', 'GiveExtraStars');
        },
        onClose: () => {
          console.log('Video ad closed.');
		  myGameInstance.SendMessage('AD_fx', 'PlayParticle');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },
  
  GetLanguage: function(){
	var lang = ysdk.environment.i18n.lang;
	
	var bufferSize = lengthBytesUTF8(lang) + 1;
	
	var buffer = _malloc(bufferSize);
	
	stringToUTF8(lang, buffer, bufferSize);
	
	return buffer;
  },

});