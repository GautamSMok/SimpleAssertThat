<?php
	require_once('SimpleAssertThat.php');
	
	Assert::that(
	Method
	::name('div')
	::withParameters(1,2)
	::inClass('Arithmatic')
	,Throws::noException()
	,'division is not working'
	,'Hello Test');
	

Assert.that(someMethod(1,1,1,1,1),Is.equalTo(5));

	class Arithmatic
	{
		
		public function someMethod($a,$b,$c,$d,$e)
		{
			return $a+$b+$c+$d+$e;
		}
		
		public function div($a,$b)
		{
			if($b==0)
			{
				throw new Exception('Division by zero');
			}
			else
			{
				throw new Exception('Division by zero');
			}
		}
	}
?>
