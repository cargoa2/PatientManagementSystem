-- Update file paths once copied to file server.  Replace where needed.
--Look over what's there.
select filepath from Documents

---update filepaths based on what's there.
--update Documents
--Set FilePath = REPLACE(FilePath, 'D:', 'C:') -- WHERE DocFileId = @GetId

-- Verify
select filepath from Documents