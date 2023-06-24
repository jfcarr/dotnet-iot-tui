ARCHIVE_NAME = SenseHatProvider.zip
PI_SERVER = raspberry-pi-server-name

default:
	@echo 'Targets:'
	@echo '  copy-pi'

copy-pi:
	-rm -f $(ARCHIVE_NAME)
	zip -r $(ARCHIVE_NAME) ../SenseHatProvider ../SenseHatLib
	scp $(ARCHIVE_NAME) $(USER)@$(PI_SERVER):/home/$(USER)
	-rm -f $(ARCHIVE_NAME)
